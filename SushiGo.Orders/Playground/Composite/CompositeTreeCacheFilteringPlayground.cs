namespace Playground.Composite;

internal sealed class CompositeTreeCacheFilteringPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- CompositePlayground--------------");
        var root = new Node();

        ITreeElement node = new Node();
        node.Add(new Leaf(10));
        node.Add(new Leaf(20));
        
        root.Add(node);
        
        ITreeElement leaf = new Leaf(30);
        
        root.Add(leaf);

        Console.WriteLine($"Root value: {root.GetValue()}");
        Console.WriteLine($"One of root child node value: {node.GetValue()}");
    }
}

public class TreeCache
{
    public List<TreeCache> Children { get; private set; } = new();
    public decimal Value { get; private set; }
    public string Type { get; private set; }
}

public class FilterByType
{
    
}


// public interface ITreeElement
// {
//     decimal GetValue();
//     
//     void Add(ITreeElement treeElement);
// }
//
// public class Node : ITreeElement
// {
//     public List<ITreeElement> Children { get; } = new();
//     
//     public decimal GetValue()
//     {
//         return Children.Sum(child => child.GetValue());
//     }
//
//     public void Add(ITreeElement treeElement)
//     {
//         Children.Add(treeElement);
//     }
// }
//
// public class Leaf(decimal value) : ITreeElement
// {
//     public decimal GetValue()
//     {
//         return value;
//     }
//
//     public void Add(ITreeElement treeElement)
//     {
//         //Cannot add elements to Leaf
//     }
// }