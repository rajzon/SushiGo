namespace Playground.Entities;

public sealed record RmaValue
{
    public string Rma { get; private set; }
    public RmaType Type { get; private set; }

    public RmaValue(string rma)
    {
        Rma = rma;
        Type = GetRmaType(rma);
    }

    private RmaType GetRmaType(string rma)
    {
        if (rma.StartsWith('S'))
        {
            return RmaType.Shop;
        }

        if (rma.StartsWith('C'))
        {
            return RmaType.Central;
        }
        
        throw new ArgumentException($"Invalid rma type: {rma}");
    }
}