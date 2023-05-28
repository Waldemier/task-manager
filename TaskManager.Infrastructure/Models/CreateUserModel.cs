namespace TaskManager.Infrastructure.Models;

public record CreateUserModel(string NickName, string FullName, string Email) : ModelBase;