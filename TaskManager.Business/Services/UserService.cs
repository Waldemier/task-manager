using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Business.Services.Abstractions;
using TaskManager.Core.UnitOfWork;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Entities;
using TaskManager.Infrastructure.Exceptions;
using TaskManager.Infrastructure.Models.Users;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Business.Services;

internal sealed class UserService : ServiceBase, IUserService
{
    private readonly IMapper _mapper;
    
    public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _mapper = mapper;
    }
    
    public async Task CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(model);
        await UnitOfWork.UserRepository.Value.CreateAsync(user, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(UpdateUserModel model, CancellationToken cancellationToken = default)
    {
        var user = await UnitOfWork.UserRepository.Value.GetEntityAsync(model.Id, cancellationToken);
        if (user is null)
            throw new ContextException($"User with {model.Id} is not found");
        _mapper.Map(model, user);
        await UnitOfWork.UserRepository.Value.UpdateAsync(user, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await UnitOfWork.UserRepository.Value.DeleteAsync(id, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await UnitOfWork.UserRepository.Value.GetEntityAsync(id, cancellationToken);
        var mapped = _mapper.Map<UserDto>(user);
        return mapped;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await UnitOfWork.UserRepository.Value
            .Get(x => true, s => s, cancellationToken)
            .ToListAsync(cancellationToken);
        return users.Select(u => _mapper.Map<UserDto>(u));
    }
}