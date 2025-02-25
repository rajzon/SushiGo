namespace Playground.Proxy;

internal sealed class ApiHttpClient : IApiHttpClient
{
    public string Get(string uri)
    {
        Console.WriteLine("Get: " + uri);
        return uri;
    }
}