using TaskModel = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Core.Repositories.Interfaces;

public interface ITaskRepository : IRepositoryBase<TaskModel>
{
    
}