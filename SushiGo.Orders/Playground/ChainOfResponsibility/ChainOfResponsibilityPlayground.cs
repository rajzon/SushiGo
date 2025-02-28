namespace Playground.ChainOfResponsibility;

internal sealed class ChainOfResponsibilityPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- ChainOfResponsibilityPlayground--------------");
        var system = new SomeSystem();
        
        //IndexMapping
        var indexMapping = system.GetMapping(new ChainOfResponsibilitySelector("index", "index", "productGroup", "department"));
        Console.WriteLine(indexMapping);
        
        //ProductGroup
        var productGroup = system.GetMapping(new ChainOfResponsibilitySelector("productGroup", "index", "productGroup", "department"));
        Console.WriteLine(productGroup);
        
        //Depertment
        var department = system.GetMapping(new ChainOfResponsibilitySelector("department", "index", "productGroup", "department"));
        Console.WriteLine(department);
    }
}

public class SomeSystem
{
    protected MappingHandler _chain;
    
    public SomeSystem()
    {
        var indexHandler = new IndexHandler();
        _chain = indexHandler;
        
        var productGroupHandler = new ProductGroupHandler();
        var departmentHandler = new DepartmentHandler();
        
        indexHandler.SetSuccessor(productGroupHandler);
        productGroupHandler.SetSuccessor(departmentHandler);
    }

    public MappingChainOfResponsibility? GetMapping(ChainOfResponsibilitySelector selector)
    {
        return _chain?.Handle(selector);
    }
}

public record MappingChainOfResponsibility(string selectedThing);

public record ChainOfResponsibilitySelector(string complaint, string indexSelector, string productGroupSelector, string departmentSelector);
public abstract class MappingHandler
{
    protected MappingHandler? _successor;
    
    public void SetSuccessor(MappingHandler successor)
    {
        _successor = successor;
    }
    
    public abstract MappingChainOfResponsibility? Handle(ChainOfResponsibilitySelector selector);
}

public class IndexHandler : MappingHandler
{
    public override MappingChainOfResponsibility? Handle(ChainOfResponsibilitySelector selector)
    {
        if (selector.complaint == selector.indexSelector)
        {
            return new MappingChainOfResponsibility(selector.indexSelector);
        }
        
        return _successor?.Handle(selector);
    }
}

public class ProductGroupHandler : MappingHandler
{
    public override MappingChainOfResponsibility? Handle(ChainOfResponsibilitySelector selector)
    {
        if (selector.complaint == selector.productGroupSelector)
        {
            return new MappingChainOfResponsibility(selector.productGroupSelector);
        }
        
        return _successor?.Handle(selector);
    }
}

public class DepartmentHandler : MappingHandler
{
    public override MappingChainOfResponsibility? Handle(ChainOfResponsibilitySelector selector)
    {
        if (selector.complaint == selector.departmentSelector)
        {
            return new MappingChainOfResponsibility(selector.departmentSelector);
        }
        
        return _successor?.Handle(selector);
    }
}