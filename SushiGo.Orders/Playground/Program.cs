// See https://aka.ms/new-console-template for more information

using Playground;
using Playground.Proxy;
using Playground.RangeIndexersAndStringManipulation;
using Playground.ResultPattern;
using Playground.Strategy;
using Playground.TaskWhenWaitAll;

Console.WriteLine("Hello, World!");
List<IPlayground> playgrounds = [new ResultPatternPlayground(), new ProxyPatternPlayground(), new RangeIndexersAndStringManipulationPlayground()];
List<IPlaygroundAsync> playgroundsAsync = [new TaskWhenWaitAllPlayground(), new StrategyPlayground()];

foreach (var playground in playgrounds)
{
    playground.Run();
}

foreach (var playgroundAsync in playgroundsAsync)
{
    if (playgroundAsync is not TaskWhenWaitAllPlayground)
    {
        await playgroundAsync.Run();
    }
}