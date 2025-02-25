namespace SushiGo.Orders.Core;

public abstract class AuditableEntity
{
    //TODO maybe generate key
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
}

public sealed class Order : AuditableEntity
{
    public string Name { get; }
}

