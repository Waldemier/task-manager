using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Models.Tasks;

namespace TaskManager.Business.Interfaces.Interfaces;

public interface ITaskService : IServiceBase
{
    Task CreateTaskAsync(CreateTaskModel model, CancellationToken cancellationToken = default);
    Task UpdateTaskAsync(UpdateTaskModel model, CancellationToken cancellationToken = default);
    Task DeleteTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<TaskDto> GetTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskDto>> GetTasksAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddAssigneeAsync(Guid id, Guid userId, Guid assigneeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserDto>> GetAssigneesAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task DeleteAssigneeAsync(Guid id, Guid userId, Guid assigneeId, CancellationToken cancellationToken = default);
}