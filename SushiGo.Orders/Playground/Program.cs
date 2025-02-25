// See https://aka.ms/new-console-template for more information

using Playground;
using Playground.Proxy;
using Playground.ResultPattern;

Console.WriteLine("Hello, World!");
List<IPlayground> playgrounds = [new ResultPatternPlayground(), new ProxyPatternPlayground()];

foreach (var playground in playgrounds)
{
    playground.Run();
}