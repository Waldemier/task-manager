using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.Users;
using TaskManager.Infrastructure.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<User> GetUser([FromRoute] Guid id, CancellationToken cancellationToken) =>
        new User();
    //await _repository.GetEntityAsync(id, cancellationToken);
    
    [HttpPost]
    public async Task CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);
}