using AutoMapper;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Entities;
using TaskManager.Infrastructure.Models.Users;

namespace TaskManager.Infrastructure.Mappings;

internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserModel, User>();
        CreateMap<UpdateUserModel, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));;
    }
}