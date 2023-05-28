namespace TaskManager.Infrastructure.Dtos;

public record UserDto(Guid Id, string NickName, string FullName, string Email, 
    ICollection<TaskDto> OwnTasks, ICollection<TaskDto> AssignedTasks) : DtoBase;