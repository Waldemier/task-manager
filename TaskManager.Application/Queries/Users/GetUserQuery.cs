using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Users;

public record GetUserQuery(Guid Id) : IRequest<UserDto>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly ILogger<GetUserQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetUserQueryHandler(ILogger<GetUserQueryHandler> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting a user with {request.Id} id");
        
        var user = await _userService.GetUserAsync(request.Id, cancellationToken);

        return user;
    }
}