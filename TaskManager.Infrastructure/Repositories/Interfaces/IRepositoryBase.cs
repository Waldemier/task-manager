using System.Linq.Expressions;
using TaskManager.Infrastructure.Entities;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Infrastructure.Repositories.Interfaces;

public interface IRepositoryBase<T> where T: EntityBase
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<T> GetEntityAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<T>> GetEntitiesAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<IQueryable<T>> Get(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}