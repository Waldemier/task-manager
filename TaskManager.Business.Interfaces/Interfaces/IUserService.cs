using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Models.Users;

namespace TaskManager.Business.Interfaces.Interfaces;

public interface IUserService : IServiceBase
{
    Task CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(UpdateUserModel model, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default);
}