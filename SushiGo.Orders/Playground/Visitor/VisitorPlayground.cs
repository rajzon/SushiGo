namespace Playground.Visitor;

internal sealed class VisitorPlayground : IPlayground
{
    
    public void Run()
    {
        Console.WriteLine("---------------- VisitorPlayground --------------");
        List<IElement> elements = [new TitleElement("This is title"), new BodyElement("This is body")];

        var htmlVisitor = new HtmlDocumentVisitor();
        foreach (var element in elements)
        {
            element.Accept(htmlVisitor);
        }
        
        var markdownVisitor = new MarkdownDocumentvisitor();
        foreach (var element in elements)
        {
            element.Accept(markdownVisitor);
        }
    }
}

public interface IVisitor
{
    void VisitTitle(TitleElement element);
    void VisitBody(BodyElement element);
}

public class HtmlDocumentVisitor : IVisitor
{
    public void VisitTitle(TitleElement element)
    {
        Console.WriteLine($"<title>{element.Value}</title>");
    }

    public void VisitBody(BodyElement element)
    {
        Console.WriteLine($"<body>{element.BodyValue}</body>");
    }
}

public class MarkdownDocumentvisitor : IVisitor
{
    public void VisitTitle(TitleElement element)
    {
        Console.WriteLine($"<markdown-title>{element.Value}</markdown-title>");
    }

    public void VisitBody(BodyElement element)
    {
        Console.WriteLine($"<markdown-body>{element.BodyValue}</markdown-body>");   
    }
}


public interface IElement
{
    void Accept(IVisitor visitor);
}


public class TitleElement(string value) : IElement
{
    public readonly string Value = value;
    
    public void Accept(IVisitor visitor)
    {
        visitor.VisitTitle(this);
        Console.WriteLine("Other title functionality");
    }
}


public class BodyElement(string bodyBodyValue) : IElement
{
    public readonly string BodyValue = bodyBodyValue;
    
    public void Accept(IVisitor visitor)
    {
        visitor.VisitBody(this);
        Console.WriteLine("Other body functionality");
    }
}