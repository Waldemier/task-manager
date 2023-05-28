namespace TaskManager.Infrastructure.Models.Tasks;

public record CreateTaskModel(Guid OwnerId, string Name, string Description) : ModelBase;