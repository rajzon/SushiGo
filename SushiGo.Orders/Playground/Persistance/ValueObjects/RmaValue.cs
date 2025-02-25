using Playground.Entities;

namespace Playground.Persistance.ValueObjects;

public sealed record RmaValue
{
    public string Rma { get; private set; }
    public RmaType Type { get; private set; }

    public RmaValue(string rma)
    {
        if (rma.Length != 10)
        {
            throw new ArgumentException($"Invalid length for rma: {rma}");
        }

        //Better approach to be use Regex - but i wanted exercise other approach
        //rma[1..] == rma.Substring(1)
        if ((!rma.StartsWith('S') && !rma.StartsWith('C')) || !rma[1..].All(char.IsDigit))
        {
            throw new ArgumentException($"Invalid rma: {rma}");
        }
        
        Rma = rma;
        Type = GetRmaType(rma);
    }

    private static RmaType GetRmaType(string rma)
    {
        if (rma.StartsWith('S'))
        {
            return RmaType.Shop;
        }

        if (rma.StartsWith('C'))
        {
            return RmaType.Central;
        }
        
        throw new ArgumentException($"Invalid rma: {rma}");
    }
}