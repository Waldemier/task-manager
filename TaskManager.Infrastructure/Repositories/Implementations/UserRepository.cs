using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.Entities;
using TaskManager.Infrastructure.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Repositories.Implementations;

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

    public IQueryable<User> GetEntitiesAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) =>
        _dbContext.Users.Where(x => ids.Contains(x.Id));

    public IQueryable<User> Get(Expression<Func<User, bool>> expression, 
        Expression<Func<User, User>> selector, CancellationToken cancellationToken) =>
        _dbContext.Users.Select(selector).Where(expression);
}