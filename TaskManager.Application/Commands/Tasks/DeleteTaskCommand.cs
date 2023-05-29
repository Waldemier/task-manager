using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Models.Tasks;

namespace TaskManager.Application.Commands.Tasks;

public record DeleteTaskCommand(Guid UserId, Guid Id) : IRequest;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ILogger<DeleteTaskCommandHandler> _logger;
    private readonly ITaskService _taskService;

    public DeleteTaskCommandHandler(ILogger<DeleteTaskCommandHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting a Task with {request.Id} begins");
        await _taskService.DeleteTaskAsync(request.Id, request.UserId, cancellationToken);
        _logger.LogInformation($"Task with {request.Id} has successfully deleted");
    }
}