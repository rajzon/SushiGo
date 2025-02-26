namespace Playground.TaskWhenWaitAll;

public interface IPlaygroundAsync
{
    Task Run();
}

internal sealed class TaskWhenWaitAllPlayground : IPlaygroundAsync
{
    public async Task Run()
    {
        Console.WriteLine("---------------- TaskWhenWaitAll --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        var task1 = DelayedTask(2000);
        var task2 = DelayedTask(5000);
        await Task.WhenAll(task1, task2); //async block (good)
        await Task.WhenAll(task1, task2); //Since tasks completed at this point there will be not execution again of this
        
        Console.WriteLine("End of TaskWhenAll");
        
        var task3 = DelayedTask(2000);
        var task4 = DelayedTask(5000);
        Task.WaitAll(task3, task4); //sync block (bad)
        
        Console.WriteLine("End of TaskWaitAll");
        
        // var task5 = DelayedTask(2000);
        // var task6 = DelayedTask(5000);
        // //Task.WhenEach() //when using .NET 9
        // await foreach (var task in Task.WhenEach(task5, task6))
        // {
        //     Console.WriteLine(task.IsFaulted
        //         ? $"Task {task.Id} failed: {task.Exception}"
        //         : $"Task {task.Id} finished. Processing...");
        // }
        // Console.WriteLine("End of TaskWhenEach");
    }

    public async Task DelayedTask(int milliseconds)
    {
        try
        {
            await Task.Delay(milliseconds);
            throw new Exception("asdasdsda");
            Console.WriteLine($"Task completed after milliseconds. {milliseconds}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}