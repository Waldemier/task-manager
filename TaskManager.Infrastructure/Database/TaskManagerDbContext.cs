using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Entities;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Infrastructure.Database;

internal class TaskManagerDbContext : DbContext, ITaskManagerDbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options): base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User Configurations

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .Property(u => u.NickName);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.NickName)
            .IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Email);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        #endregion

        #region Task Configurations

        modelBuilder.Entity<Task>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<Task>()
            .Property(t => t.Name);
        modelBuilder.Entity<Task>()
            .Property(t => t.Description);

        modelBuilder.Entity<Task>()
            .HasOne(t => t.Owner)
            .WithMany(u => u.OwnTasks)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Task>()
            .HasMany(t => t.Assignees)
            .WithMany(u => u.AssignedTasks);

        #endregion
    }
}