namespace TaskManager.Infrastructure.Entities;

public abstract class EntityBase
{
    public Guid Id { get; init; }
    public DateTime CreationDate { get; private set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    protected abstract void Validate();
}