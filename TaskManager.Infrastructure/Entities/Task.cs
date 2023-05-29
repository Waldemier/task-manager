using GuardNet;

namespace TaskManager.Infrastructure.Entities;

public class Task : EntityBase
{
    private const int MaxNameLength = 255;
    private const int MinNameLength = 1;
    
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public ICollection<User> Assignees { get; set; }

    protected Task(): base()
    {
        Assignees = new List<User>();
    }
    
    public Task(string name, string description, User owner, ICollection<User>? assignees = null): base()
    {
        Name = name;
        Description = description;
        Owner = owner;
        Assignees = assignees ?? new List<User>();
        Validate();
    }
    
    public Task(string name, string description, Guid ownerId, ICollection<User>? assignees = null): base()
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
        Assignees = assignees ?? new List<User>();
        Validate();
    }

    protected override void Validate()
    {
        Guard.NotGreaterThan(Name.Length, MaxNameLength, new ArgumentException($"{nameof(Name)} is greater than {MaxNameLength} characters"));
        Guard.NotLessThan(Name.Length, MinNameLength, new ArgumentException($"{nameof(Name)} is less than {MaxNameLength} characters"));
    }
}