using MediatR;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Users;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;