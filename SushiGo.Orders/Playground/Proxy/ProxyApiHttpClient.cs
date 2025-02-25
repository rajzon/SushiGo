namespace Playground.Proxy;

public class ProxyApiHttpClient(IApiHttpClient apiHttpClient) : IApiHttpClient
{
    public string Get(string uri)
    {
        Console.WriteLine("LOG from PROXY");
        return apiHttpClient.Get(uri);
    }
}