using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;

namespace TaskManager.Application.Commands.Users;

public record DeleteUserCommand(Guid Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IUserService _userService;

    public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting a user with {request.Id} id begins");
        
        await _userService.DeleteUserAsync(request.Id, cancellationToken);
    }
}