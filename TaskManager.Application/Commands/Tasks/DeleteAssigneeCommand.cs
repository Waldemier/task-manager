using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;

namespace TaskManager.Application.Commands.Tasks;

public record DeleteAssigneeCommand(Guid Id, Guid UserId, Guid AssigneeId) : IRequest;

public class DeleteAssigneeCommandHandler : IRequestHandler<DeleteAssigneeCommand>
{
    private readonly ILogger<DeleteAssigneeCommandHandler> _logger;
    private readonly ITaskService _taskService;

    public DeleteAssigneeCommandHandler(ILogger<DeleteAssigneeCommandHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task Handle(DeleteAssigneeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deletion of an assignee with {request.AssigneeId} id from {request.Id} task id begins");
        await _taskService.DeleteAssigneeAsync(request.Id, request.UserId, request.AssigneeId, cancellationToken);
    }
}