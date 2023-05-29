namespace TaskManager.Infrastructure.Models.Tasks;

public record CreateTaskModel(string Name, string Description, Guid OwnerId) : ModelBase;