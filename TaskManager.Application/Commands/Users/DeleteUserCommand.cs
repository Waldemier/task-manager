using MediatR;

namespace TaskManager.Application.Commands.Users;

public record DeleteUserCommand(Guid Id) : IRequest;
