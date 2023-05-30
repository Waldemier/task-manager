using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Business.Services.Abstractions;
using TaskManager.Core.UnitOfWork;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Entities;
using TaskManager.Infrastructure.Exceptions;
using TaskManager.Infrastructure.Models.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManager.Infrastructure.Entities.Task;
namespace TaskManager.Business.Services;

internal sealed class TaskService : ServiceBase, ITaskService
{
    private readonly IMapper _mapper;
    
    public TaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _mapper = mapper;
    }

    public async Task CreateTaskAsync(CreateTaskModel model, CancellationToken cancellationToken = default)
    {
        var task = _mapper.Map<TaskEntity>(model);
        await UnitOfWork.TaskRepository.Value.CreateAsync(task, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTaskAsync(UpdateTaskModel model, CancellationToken cancellationToken = default)
    {
        var entity = await UnitOfWork.TaskRepository.Value.GetEntityAsync(model.Id, cancellationToken);

        if (entity is null)
            throw new ContextException($"Task with {model.Id} id does not exist");

        CheckRestrictionsAccess(entity, model.Id, model.UserId);
        
        _mapper.Map(model, entity);
        await UnitOfWork.TaskRepository.Value.UpdateAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var entity = await UnitOfWork.TaskRepository.Value.GetEntityAsync(id, cancellationToken);

        if (entity is null)
            throw new ContextException($"Task with {id} id does not exist");
        
        CheckRestrictionsAccess(entity, id, userId, true);

        await UnitOfWork.TaskRepository.Value.DeleteAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<TaskDto> GetTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var entity = await UnitOfWork.TaskRepository.Value.Get(c => c.Id == id && (c.OwnerId == userId || c.Assignees.Any(u => u.Id == userId)), 
            s => 
                new TaskEntity(s.Name, s.Description, s.Owner, 
                    s.Assignees.Select(u => new User(u.NickName, u.FullName, u.Email)
                    {
                        Id = u.Id
                    }).ToList())
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId
                })
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            throw new ContextException($"Task with {id} id does not exist");
        
        CheckRestrictionsAccess(entity, id, userId);

        return _mapper.Map<TaskDto>(entity);
    }

    public async Task<IEnumerable<TaskDto>> GetTasksAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var entities = await UnitOfWork.TaskRepository.Value.Get(
                c => (c.OwnerId == userId || c.Assignees.Any(u => u.Id == userId)),
                s => s)
            .OrderByDescending(t => t.CreationDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        foreach (var entity in entities)
        {
            await UnitOfWork.TaskRepository.Value.LoadNavigationPropertyExplicitly(entity, t => t.Owner,
                cancellationToken);
            
            await UnitOfWork.TaskRepository.Value.LoadNavigationCollectionExplicitly(entity, t => t.Assignees,
                cancellationToken);
        }
        
        return _mapper.Map<IEnumerable<TaskDto>>(entities);
    }

    public async Task AddAssigneeAsync(Guid id, Guid userId, Guid assigneeId, CancellationToken cancellationToken = default)
    {
        var entity = await UnitOfWork.TaskRepository.Value.Get(c => c.Id == id && (c.OwnerId == userId || c.Assignees.Any(u => u.Id == userId)), 
                s => s)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (entity is null)
            throw new ContextException($"Task with {id} id is not found");
        
        await UnitOfWork.TaskRepository.Value.LoadNavigationCollectionExplicitly(entity, t => t.Assignees,
            cancellationToken);
        
        var assignee = await UnitOfWork.UserRepository.Value.GetEntityAsync(assigneeId, cancellationToken);

        if (assignee is null)
            throw new ContextException($"An assignee with {assigneeId} id is not found or you don't have an access to edit it");
        
        entity.Assignees.Add(assignee);
        
        await UnitOfWork.TaskRepository.Value.UpdateAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> GetAssigneesAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var entity = await UnitOfWork.TaskRepository.Value.Get(c => c.Id == id && (c.OwnerId == userId || c.Assignees.Any(u => u.Id == userId)), 
                s => s)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            throw new ContextException($"Task with {id} id is not found or you don't have an access to read it");
        
        await UnitOfWork.TaskRepository.Value.LoadNavigationCollectionExplicitly(entity, t => t.Assignees,
            cancellationToken);
        
        return _mapper.Map<IEnumerable<UserDto>>(entity?.Assignees);
    }

    public async Task DeleteAssigneeAsync(Guid id, Guid userId, Guid assigneeId, CancellationToken cancellationToken = default)
    {
        var entity = await UnitOfWork.TaskRepository.Value.Get(c => c.Id == id && (c.OwnerId == userId || c.Assignees.Any(u => u.Id == userId)), 
                s => s)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (entity is null)
            throw new ContextException($"Task with {id} id is not found or you don't have an access to delete it");

        await UnitOfWork.TaskRepository.Value.LoadNavigationCollectionExplicitly(entity, t => t.Assignees,
            cancellationToken);
        
        var assignee = entity.Assignees.FirstOrDefault(u => u.Id == assigneeId);
        
        if (assignee is null)
            throw new ContextException($"An assignee with {assigneeId} id is not found");
        
        entity.Assignees.Remove(assignee);
        
        await UnitOfWork.TaskRepository.Value.UpdateAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void CheckRestrictionsAccess(TaskEntity entity, Guid taskId, Guid userId, bool deleteAction = false)
    {
        if (entity.OwnerId != userId || !deleteAction && (entity.Assignees.Count > 0 && entity.Assignees.All(u => u.Id != userId)))
            throw new ForbiddenException($"User with {userId} id is forbidden to update a Task with {taskId} id");
    }
}