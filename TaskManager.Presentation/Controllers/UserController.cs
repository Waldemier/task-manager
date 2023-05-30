using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Queries.Users;
using TaskManager.Infrastructure.Dtos;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken) =>
        await _mediator.Send(new GetUsersQuery(), cancellationToken);
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute] Guid id, CancellationToken cancellationToken) =>
        await _mediator.Send(new GetUserQuery(id), cancellationToken);

    [HttpPost]
    public async Task CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);
    
    [HttpPut]
    public async Task UpdateUser([FromBody] UpdateUserCommand command, CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);
    
    [HttpDelete("{id:guid}")]
    public async Task DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken) =>
        await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
}