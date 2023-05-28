using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Models.Users;

namespace TaskManager.Application.Commands.Users;

public record CreateUserCommand(string NickName, string FullName, string Email) : IRequest;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUserService _userService;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creation of a user with {request.NickName} nickname begins");
        
        await _userService.CreateUserAsync(new CreateUserModel(request.NickName, request.FullName, request.Email), cancellationToken);
        
        _logger.LogInformation($"User with {request.NickName} nickname has been successfully created");
    }
}
