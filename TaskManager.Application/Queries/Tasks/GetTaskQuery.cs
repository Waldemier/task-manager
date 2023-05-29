using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Tasks;

public record GetTaskQuery(Guid UserId, Guid Id) : IRequest<TaskDto>;

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto>
{
    private readonly ILogger<GetTaskQueryHandler> _logger;
    private readonly ITaskService _taskService;

    public GetTaskQueryHandler(ILogger<GetTaskQueryHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task<TaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken) =>
        await _taskService.GetTaskAsync(request.Id, request.UserId, cancellationToken);
}