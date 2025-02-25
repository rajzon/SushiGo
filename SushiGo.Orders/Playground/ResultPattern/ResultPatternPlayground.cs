using System.Text.Json;

namespace Playground.ResultPattern;

internal class ResultPatternPlayground : IPlayground
{
    public void Run()
    {
        // test.Data = test.Data + 1;
        var result = ErrorOr<int>.Success(10);
        var result2 = ErrorOr<int>.Failure("failure message");
        Console.WriteLine("---------------- ResultPattern --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        Console.WriteLine($"result: {JsonSerializer.Serialize(result)}");
        Console.WriteLine($"result: {result.IsSuccess}");
        Console.WriteLine($"result2: {JsonSerializer.Serialize(result2)}");
        Console.WriteLine($"result2: {result2.IsSuccess}");
    }
}