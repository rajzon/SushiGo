// See https://aka.ms/new-console-template for more information

using Playground;
using Playground.Proxy;
using Playground.ResultPattern;
using Playground.TaskWhenWaitAll;

Console.WriteLine("Hello, World!");
List<IPlayground> playgrounds = [new ResultPatternPlayground(), new ProxyPatternPlayground()];
List<IPlaygroundAsync> playgroundsAsync = [new TaskWhenWaitAllPlayground()];

foreach (var playground in playgrounds)
{
    playground.Run();
}

foreach (var playgroundAsync in playgroundsAsync)
{
    await playgroundAsync.Run();
}