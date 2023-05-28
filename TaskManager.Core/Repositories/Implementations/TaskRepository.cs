using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Database;
using TaskManager.Core.Repositories.Interfaces;
using TaskModel = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Core.Repositories.Implementations;

internal sealed class TaskRepository : ITaskRepository
{
    private readonly ITaskManagerDbContext _dbContext;

    public TaskRepository(ITaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(TaskModel entity, CancellationToken cancellationToken) =>
        await _dbContext.Tasks.AddAsync(entity, cancellationToken);

    public async Task UpdateAsync(TaskModel entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _dbContext.Tasks.Update(entity), cancellationToken);

    public async Task DeleteAsync(TaskModel entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _dbContext.Tasks.Remove(entity), cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetEntityAsync(id, cancellationToken);
        
        if (entity != null)
            await Task.Run(() => _dbContext.Tasks.Remove(entity), cancellationToken);
    }

    public async Task<TaskModel?> GetEntityAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public IQueryable<TaskModel> GetEntitiesAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) =>
        _dbContext.Tasks.Where(x => ids.Contains(x.Id));

    public IQueryable<TaskModel> Get(Expression<Func<TaskModel, bool>> expression, 
        Expression<Func<TaskModel, TaskModel>> selector, CancellationToken cancellationToken) =>
        _dbContext.Tasks.Select(selector).Where(expression);
}