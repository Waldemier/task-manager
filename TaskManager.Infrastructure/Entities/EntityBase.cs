namespace TaskManager.Infrastructure.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; }
    public DateTime CreationDate { get; private set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        Validate();
    }

    protected abstract void Validate();
}