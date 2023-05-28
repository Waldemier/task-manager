using TaskModel = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Infrastructure.Repositories.Interfaces;

public interface ITaskRepository : IRepositoryBase<TaskModel>
{
    
}