using System.Linq.Expressions;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Database;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Business.Services;

internal class TaskRepository : ITaskRepository
{
    private readonly ITaskManagerDbContext _dbContext;

    public TaskRepository(ITaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task CreateAsync(Task entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Task entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Task entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Task> GetEntityAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Task>> GetEntitiesAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Task>> Get(Expression<Func<Task, bool>> expression, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}