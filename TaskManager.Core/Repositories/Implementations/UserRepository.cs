using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Database;
using TaskManager.Core.Repositories.Interfaces;
using TaskManager.Infrastructure.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Core.Repositories.Implementations;

internal sealed class UserRepository : IUserRepository
{
    private readonly ITaskManagerDbContext _dbContext;

    public UserRepository(ITaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(User entity, CancellationToken cancellationToken) =>
        await _dbContext.Users.AddAsync(entity, cancellationToken);

    public async Task UpdateAsync(User entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _dbContext.Users.Update(entity), cancellationToken);

    public async Task DeleteAsync(User entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _dbContext.Users.Remove(entity), cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetEntityAsync(id, cancellationToken);
        
        if (entity != null)
            await Task.Run(() => _dbContext.Users.Remove(entity), cancellationToken);
    }

    public async Task<User?> GetEntityAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public IQueryable<User> GetEntitiesAsync(IEnumerable<Guid> ids) =>
        _dbContext.Users.Where(x => ids.Contains(x.Id));

    public IQueryable<User> Get(Expression<Func<User, bool>> expression, Expression<Func<User, User>> selector) =>
        _dbContext.Users.Select(selector).Where(expression);
    
    public async Task LoadNavigationPropertyExplicitly<TProperty>(User entity,
        Expression<Func<User, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty: class =>
        await _dbContext.Users.Entry(entity).Reference(relation!).LoadAsync(cancellationToken);
    
    public async Task LoadNavigationCollectionExplicitly<TProperty>(User entity, Expression<Func<User, IEnumerable<TProperty>>> relation, 
        CancellationToken cancellationToken = default) where TProperty: class =>
        await _dbContext.Users.Entry(entity).Collection(relation!).LoadAsync(cancellationToken);
}