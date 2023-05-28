using AutoMapper;
using TaskManager.Infrastructure.Dtos;
using TaskManager.Infrastructure.Models.Tasks;
using Task = TaskManager.Infrastructure.Entities.Task;

namespace TaskManager.Infrastructure.Mappings;

internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>();
        CreateMap<CreateTaskModel, Task>();
        CreateMap<UpdateTaskModel, Task>()
            .ForMember(d => d.OwnerId, 
                op => op.Ignore());
    }
}