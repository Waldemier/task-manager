namespace TaskManager.Infrastructure.Models.Users;

public record UpdateUserModel(Guid Id, string NickName, string FullName, string Email) : ModelBase;