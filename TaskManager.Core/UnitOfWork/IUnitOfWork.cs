using TaskManager.Core.Repositories.Interfaces;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Core.UnitOfWork;

public interface IUnitOfWork
{
    Lazy<IUserRepository> UserRepository { get; }
    Lazy<ITaskRepository> TaskRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}