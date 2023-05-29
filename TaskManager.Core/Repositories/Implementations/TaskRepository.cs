using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Database;
using TaskManager.Core.Repositories.Interfaces;
using TaskEntity = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Core.Repositories.Implementations;

internal sealed class TaskRepository : ITaskRepository
{
    private readonly ITaskManagerDbContext _dbContext;

    public TaskRepository(ITaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(TaskEntity entity, CancellationToken cancellationToken) =>
        await _dbContext.Tasks.AddAsync(entity, cancellationToken);

    public async Task UpdateAsync(TaskEntity entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _dbContext.Tasks.Update(entity), cancellationToken);

    public async Task DeleteAsync(TaskEntity entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _dbContext.Tasks.Remove(entity), cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetEntityAsync(id, cancellationToken);
        
        if (entity != null)
            await Task.Run(() => _dbContext.Tasks.Remove(entity), cancellationToken);
    }

    public async Task<TaskEntity?> GetEntityAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public IQueryable<TaskEntity> GetEntitiesAsync(IEnumerable<Guid> ids) =>
        _dbContext.Tasks.Where(x => ids.Contains(x.Id));

    public IQueryable<TaskEntity> Get(Expression<Func<TaskEntity, bool>> expression, Expression<Func<TaskEntity, TaskEntity>> selector) =>
        _dbContext.Tasks.Select(selector).Where(expression);

    public async Task LoadNavigationPropertyExplicitly<TProperty>(TaskEntity entity,
        Expression<Func<TaskEntity, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty: class =>
        await _dbContext.Tasks.Entry(entity).Reference(relation!).LoadAsync(cancellationToken);
    public async Task LoadNavigationCollectionExplicitly<TProperty>(TaskEntity entity, Expression<Func<TaskEntity, IEnumerable<TProperty>>> relation, 
        CancellationToken cancellationToken = default) where TProperty: class =>
        await _dbContext.Tasks.Entry(entity).Collection(relation!).LoadAsync(cancellationToken);
}