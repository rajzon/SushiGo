namespace Playground.Entities;

public sealed class Complaint : Entity, IAggregate
{
    public RmaValue Rma { get; }
    
    private Complaint() { }

    public Complaint(string rma)
    {
        Rma = new RmaValue(rma);
    }
}