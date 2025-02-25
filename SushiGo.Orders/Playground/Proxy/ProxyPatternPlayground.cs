namespace Playground.Proxy;

internal sealed class ProxyPatternPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- Proxy --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        var mainObj = new ApiHttpClient();
        var uri = "test uri";
        
        Console.WriteLine(mainObj.Get(uri));
        var proxiedObj = new ProxyApiHttpClient(mainObj);
        Console.WriteLine(proxiedObj.Get(uri));
    }
}