using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Tasks;
using TaskManager.Application.Queries.Tasks;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Presentation.Controllers;

// TODO: Add Annotations
[ApiController]
[Route("api/users/{userId:guid}/tasks")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskDto>> GetTasks([FromRoute] Guid userId, [FromQuery] int pageNumber, 
        [FromQuery] int pageSize, CancellationToken cancellationToken) =>
        await _mediator.Send(new GetTasksQuery(userId, pageNumber, pageSize), cancellationToken);
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDto>> GetTask([FromRoute] Guid userId, [FromRoute] Guid id, CancellationToken cancellationToken) =>
        await _mediator.Send(new GetTaskQuery(userId, id), cancellationToken);

    [HttpPost]
    public async Task CreateTask([FromRoute] Guid userId, [FromBody] CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        command.OwnerId = userId;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    public async Task UpdateTask([FromRoute] Guid userId, [FromBody] UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {
        command.UserId = userId;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteTask([FromRoute] Guid userId, [FromRoute] Guid id, CancellationToken cancellationToken) =>
        await _mediator.Send(new DeleteTaskCommand(userId, id), cancellationToken);
}