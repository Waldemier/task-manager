using MediatR;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Users;

public class GetUsersQuery : IRequest<IEnumerable<TaskDto>>
{
    
}