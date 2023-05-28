using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Business.Interfaces.Interfaces;

public interface ITaskRepository : IRepositoryBase<Task>
{
    
}