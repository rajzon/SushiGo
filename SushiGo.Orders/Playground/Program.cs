// See https://aka.ms/new-console-template for more information

using Playground;
using Playground.Adapter;
using Playground.ChainOfResponsibility;
using Playground.Composite;
using Playground.Dates;
using Playground.Decorator;
using Playground.Proxy;
using Playground.RangeIndexersAndStringManipulation;
using Playground.ResultPattern;
using Playground.Strategy;
using Playground.TaskWhenWaitAll;
using Playground.TemplateMethod;
using Playground.Visitor;

Console.WriteLine("Hello, World!");

List<IPlayground> playgrounds =
[
    new ResultPatternPlayground(), 
    new ProxyPatternPlayground(), 
    new RangeIndexersAndStringManipulationPlayground(),
    new DatesPlayground(), 
    new VisitorPlayground(), 
    new DecoratorPlayground(),
    new AdapterPlayground(),
    new TemplateMethodPlayground(),
    new CompositePlayground(),
    new ChainOfResponsibilityPlayground()
];
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