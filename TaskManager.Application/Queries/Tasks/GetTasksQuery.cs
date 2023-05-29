using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Tasks;

public record GetTasksQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<IEnumerable<TaskDto>>;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ILogger<GetTasksQueryHandler> _logger;
    private readonly ITaskService _taskService;

    public GetTasksQueryHandler(ILogger<GetTasksQueryHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken) =>
        await _taskService.GetTasksAsync(request.UserId, request.PageNumber, request.PageSize, cancellationToken);
}