using System.Linq.Expressions;
using TaskManager.Infrastructure.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Core.Repositories.Interfaces;

public interface IRepositoryBase<T> where T: EntityBase
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T?> GetEntityAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<T> GetEntitiesAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    IQueryable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, T>> selector, CancellationToken cancellationToken = default);
}