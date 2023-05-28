namespace TaskManager.Infrastructure.Models.Users;

public record CreateUserModel(string NickName, string FullName, string Email) : ModelBase;