namespace Playground.Decorator;

internal sealed class DecoratorPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- DecoratorPlayground --------------");
        var decoratedService = new LoggingDecorator(new MyService());
        decoratedService.DoSomething("My content is Lorem ipsum dolor sit amet");
    }
}


public interface IMyService
{
    bool DoSomething(string content);
}

public class MyService : IMyService
{
    public bool DoSomething(string content)
    {
        Console.WriteLine("This is my service Do Something with content");
        Console.WriteLine(content);
        
        return true;
    }
}

public class LoggingDecorator(IMyService service) : IMyService
{
    public bool DoSomething(string content)
    {
        Console.WriteLine("LoggingDecorator: Logging Before result from service");
        var result = service.DoSomething(content);
        Console.WriteLine("LoggingDecorator: Logging After result from service");
        return result;
    }
}