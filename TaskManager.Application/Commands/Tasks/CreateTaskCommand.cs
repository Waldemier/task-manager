using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Models.Tasks;

namespace TaskManager.Application.Commands.Tasks;

public record CreateTaskCommand(string Name, string Description) : IRequest
{
    public Guid OwnerId { get; set; }
}

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
{
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly ITaskService _taskService;

    public CreateTaskCommandHandler(ILogger<CreateTaskCommandHandler> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creation of a task with {request.Name} begins");
        await _taskService.CreateTaskAsync(new CreateTaskModel(request.Name, request.Description, request.OwnerId), cancellationToken);
        _logger.LogInformation($"Task with {request.Name} has successfully created");
    }
}