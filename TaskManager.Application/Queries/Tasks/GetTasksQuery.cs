using MediatR;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Tasks;

public class GetTasksQuery : IRequest<IEnumerable<TaskDto>>
{
    
}