using AutoMapper;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Business.Services.Abstractions;
using TaskManager.Core.UnitOfWork;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Models.Tasks;

namespace TaskManager.Business.Services;

internal sealed class TaskService : ServiceBase, ITaskService
{
    private readonly IMapper _mapper;
    
    public TaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _mapper = mapper;
    }

    public Task CreateTaskAsync(CreateTaskModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskAsync(UpdateTaskModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> GetTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDto>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}