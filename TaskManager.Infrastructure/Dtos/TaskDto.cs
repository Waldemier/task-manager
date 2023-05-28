namespace TaskManager.Infrastructure.Dtos;

public record TaskDto(Guid Id, string Name, string Description, UserDto Owner, ICollection<UserDto> Assignees) : DtoBase;