namespace TaskManager.Infrastructure.Models.Tasks;

public record UpdateTaskModel(Guid Id, Guid UserId, string Name, string Description) : ModelBase;