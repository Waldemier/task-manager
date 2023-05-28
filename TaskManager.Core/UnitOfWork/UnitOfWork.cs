using TaskManager.Core.Database;
using TaskManager.Core.Repositories.Implementations;
using TaskManager.Core.Repositories.Interfaces;

namespace TaskManager.Core.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ITaskManagerDbContext _dbContext;
    public Lazy<IUserRepository> UserRepository { get; private set; }
    public Lazy<ITaskRepository> TaskRepository { get; private set; }

    public UnitOfWork(ITaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
        UserRepository = new Lazy<IUserRepository>(new UserRepository(_dbContext));
        TaskRepository = new Lazy<ITaskRepository>(new TaskRepository(_dbContext));
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}