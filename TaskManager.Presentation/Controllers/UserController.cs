using Microsoft.AspNetCore.Mvc;
using TaskManager.Infrastructure.Entities;

namespace TaskManager.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser([FromRoute] Guid id, CancellationToken cancellationToken) =>
        new User();
    //await _repository.GetEntityAsync(id, cancellationToken);
}