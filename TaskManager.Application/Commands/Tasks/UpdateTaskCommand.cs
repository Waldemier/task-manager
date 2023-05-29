using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Models.Tasks;

namespace TaskManager.Application.Commands.Tasks;

public record UpdateTaskCommand(Guid Id, string Name, string Description) : IRequest
{
    public Guid UserId { get; set; }
}

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ILogger<UpdateTaskCommandHandler> _logger;
    private readonly ITaskService _taskService;

    public UpdateTaskCommandHandler(ILogger<UpdateTaskCommandHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating a task with {request.Id} begins");
        await _taskService.UpdateTaskAsync(new UpdateTaskModel(request.Id, request.UserId, request.Name, request.Description), cancellationToken);
        _logger.LogInformation($"Task with {request.Id} has successfully updated");
    }
}