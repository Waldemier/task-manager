using System.Linq.Expressions;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.Entities;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Business.Services;

internal class UserRepository : IUserRepository
{
    private readonly ITaskManagerDbContext _dbContext;

    public UserRepository(ITaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task CreateAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetEntityAsync(Guid id, CancellationToken cancellationToken)
    {
        return System.Threading.Tasks.Task.FromResult(new User("test-nickname-1", "test-name-1", "test@gmail.com"));
    }

    public Task<IQueryable<User>> GetEntitiesAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> Get(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}