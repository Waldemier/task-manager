using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;

namespace TaskManager.Application.Commands.Tasks;

public record AddAssigneeCommand(Guid Id, Guid UserId, Guid AssigneeId) : IRequest;

public class AddAssigneeCommandHandler : IRequestHandler<AddAssigneeCommand>
{
    private readonly ILogger<AddAssigneeCommandHandler> _logger;
    private readonly ITaskService _taskService;

    public AddAssigneeCommandHandler(ILogger<AddAssigneeCommandHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task Handle(AddAssigneeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Adding an assignee with {request.AssigneeId} id from {request.Id} task id");
        await _taskService.AddAssigneeAsync(request.Id, request.UserId, request.AssigneeId, cancellationToken);
    }
}