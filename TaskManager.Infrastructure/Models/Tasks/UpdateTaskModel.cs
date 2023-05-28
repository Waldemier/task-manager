namespace TaskManager.Infrastructure.Models.Tasks;

public record UpdateTaskModel(Guid Id, Guid OwnerId, string Name, string Description) : ModelBase;