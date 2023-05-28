using MediatR;

namespace TaskManager.Application.Commands.Users;

public record CreateUserCommand(string NickName, string FullName, string Email) : IRequest;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    public CreateUserCommandHandler()
    {
        
    }
    
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
