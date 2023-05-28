using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Entities;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _repository;

    public UserController(ILogger<UserController> logger, IUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser([FromRoute] Guid id, CancellationToken cancellationToken) =>
        await _repository.GetEntityAsync(id, cancellationToken);
}