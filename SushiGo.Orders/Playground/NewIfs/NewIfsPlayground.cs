namespace Playground.NewIfs;

public class ObjectNewIfs
{
    public int SomeNumber { get; set; }
}

internal sealed class NewIfsPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- NewIfsPlayground --------------");
        var number = new Random().Next(30);
        var obj = new ObjectNewIfs() { SomeNumber = 21 };
        
        //Only Constant value 
        // if (obj is { SomeNumber: number }) //fail
        // {
        //     
        // }
        if (obj is { SomeNumber: 10 }) //OK
        {
            Console.WriteLine($"Number: {obj.SomeNumber}");
        }
        
        // if (obj.SomeNumber is > number) //Fail, only constant value
        // {
        //     
        // }
        
        if (obj?.SomeNumber is > 10 and < 20) //OK
        {
            
        }
        
        //NUll check and comparinson
        if (obj is {SomeNumber: > 10 and < 20}) //OK
        {
            
        }
    }
}