using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using GuardNet;

namespace TaskManager.Infrastructure.Entities;

public class User : EntityBase
{
    private int MinFullNameLength = 3;
    private int MaxFullNameLength = 150;
    private int MaxNickNameLength = 25;
    private int MinNickNameLength = 6;
    
    public string NickName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public ICollection<Task> OwnTasks { get; set; }
    public ICollection<Task> AssignedTasks { get; set; }

    protected User(): base()
    {
        OwnTasks = new List<Task>();
        AssignedTasks = new List<Task>();
    }
    
    public User(string nickName, string fullName, string email): base()
    {
        NickName = nickName;
        FullName = fullName;
        Email = email;
        OwnTasks = new List<Task>();
        AssignedTasks = new List<Task>();
        Validate();
    }

    protected override void Validate()
    {
        Guard.For(() => Regex.IsMatch(Email, "/^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$/g"),
            new ArgumentException($"{nameof(Email)} is not valid"));
        Guard.NotGreaterThan(FullName.Length, MaxFullNameLength, 
            new ArgumentException($"{nameof(FullName)} is greater than {MaxFullNameLength} characters"));
        Guard.NotLessThan(FullName.Length, MinFullNameLength, 
            new ArgumentException($"{nameof(FullName)} is less than {MinFullNameLength} characters"));
        Guard.NotGreaterThan(NickName.Length, MaxNickNameLength, 
            new ArgumentException($"{nameof(NickName)} is greater than {MaxNickNameLength} characters"));
        Guard.NotLessThan(NickName.Length, MinNickNameLength, 
            new ArgumentException($"{nameof(NickName)} is less than {MinNickNameLength} characters"));
    }
}