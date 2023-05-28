using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Application.Queries.Users;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly ILogger<GetUsersQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetUsersQueryHandler(ILogger<GetUsersQueryHandler> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting all users");
        
        var users = await _userService.GetUsersAsync(cancellationToken);

        return users;
    }
}