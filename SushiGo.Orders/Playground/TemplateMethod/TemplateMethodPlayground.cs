namespace Playground.TemplateMethod;

internal sealed class TemplateMethodPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- TemplateMethodPlayground --------------");
        var mappingSelectorByComplaint = new GetMappingByComplaintSelector();
        mappingSelectorByComplaint.GetMapping("complaint selector");
        
        var mappingSelector = new GetMappingSelector();
        mappingSelector.GetMapping("mapping selector");
    }
    
    
}

public record Mapping(string ProductIndex);

public abstract class MappingSelectionTemplate
{
    public Mapping GetMapping(string selection)
    {
        LogOperation("validating request");
        var valid = Validate(selection);
        
        LogOperation("getting mapping");
        var result = GetMappingInternal(selection);

        return result;
    }

    protected virtual void LogOperation(string message)
    {
        Console.WriteLine(message);
    }
    
    protected abstract bool Validate(string selection);

    protected abstract Mapping? GetMappingInternal(string selection);
}

public class GetMappingByComplaintSelector : MappingSelectionTemplate
{
    protected override bool Validate(string selection)
    {
        LogOperation("HUGE LOGIC");
        return true;
    }

    protected override Mapping? GetMappingInternal(string selection)
    {
        LogOperation("GetMappingInternal based on complaint");
        return new Mapping(string.Empty);
    }
}

public class GetMappingSelector : MappingSelectionTemplate
{
    protected override bool Validate(string selection)
    {
        LogOperation("HUGE LOGIC");
        return true;
    }

    protected override Mapping? GetMappingInternal(string selection)
    {
        LogOperation("GetMappingInternal based on Mapping");
        return new Mapping(string.Empty);
    }
}

