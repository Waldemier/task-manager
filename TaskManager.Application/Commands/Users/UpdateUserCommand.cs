using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Models.Users;

namespace TaskManager.Application.Commands.Users;

public record UpdateUserCommand(Guid Id, string? NickName, string? FullName, string? Email) : IRequest;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IUserService _userService;

    public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating a user with {request.NickName} nickname begins");
        
        await _userService.UpdateUserAsync(new UpdateUserModel(request.Id, request.NickName, request.FullName, request.Email), cancellationToken);
    }
}