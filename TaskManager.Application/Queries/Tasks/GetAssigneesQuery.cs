using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Tasks;

public record GetAssigneesQuery(Guid Id, Guid UserId) : IRequest<IEnumerable<UserDto>>;

public class GetAssigneesQueryHandler : IRequestHandler<GetAssigneesQuery, IEnumerable<UserDto>>
{
    private readonly ILogger<GetAssigneesQueryHandler> _logger;
    private readonly ITaskService _taskService;

    public GetAssigneesQueryHandler(ILogger<GetAssigneesQueryHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task<IEnumerable<UserDto>> Handle(GetAssigneesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting assignees for {request.Id} task id begins");
        return await _taskService.GetAssigneesAsync(request.Id, request.UserId, cancellationToken);
    }
}