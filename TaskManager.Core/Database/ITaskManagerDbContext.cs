using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Entities;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Core.Database;

internal interface ITaskManagerDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Task> Tasks { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}