using MediatR;

namespace TaskManager.Application.Commands.Users;

public record UpdateUserCommand(Guid Id, string NickName, string FullName, string Email) : IRequest;