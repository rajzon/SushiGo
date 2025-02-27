namespace Playground.RangeIndexersAndStringManipulation;

internal sealed class RangeIndexersAndStringManipulationPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- RangeIndexersAndStringManipulation --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        Console.WriteLine("------------------- StringManipulation ----------------");
        var str = "testowy string";
        Console.WriteLine($"Original string: '{str}'");
        
        Console.WriteLine($"Take first 4 chars: (str[..4]) '{str[..4]}'");    
        Console.WriteLine($"Start AFTER 4 char(counting from 1 NOT 0) up to the end: (str[4..]) '{str[4..]}'");     
        Console.WriteLine($"Start AFTER 2nd char and on 4char(including): (str[2..4]) '{str[2..4]}'");

        Console.WriteLine($"Take last char: (str[^1]) '{str[^1]}'");
        Console.WriteLine($"Take char 3rd from end: (str[^3]) '{str[^3]}'");
        Console.WriteLine($"Take last 2 chars: (str[^2..]) '{str[^2..]}'");
        Console.WriteLine($"Take last 5 chars: (str[^5..]) '{str[^5..]}'");
        Console.WriteLine($"Start from last 5 char up to last 3rd char: (str[^5..^3]) '{str[^5..^3]}'");
        Console.WriteLine($"Start from begining up to last 3rd char: (str[..^3]) '{str[..^3]}'");
        
        var isDigitsOnly = str.All(char.IsDigit);
        Console.WriteLine($"IsAllDigitsOnly: (str.All(char.IsDigit)) '{isDigitsOnly}'");

        Console.WriteLine("------------------- ArrayManipulation ----------------");
        List<string> lines = ["one", "two", "three", "four", "five"];
        Console.WriteLine($"Original list: '{string.Join(',', lines)}'");
        
        Console.WriteLine($"Start AFTER first element up to end: (lines[1..]) '{string.Join(',', lines[1..])}'");
        Console.WriteLine($"Start AFTER first element up to 3rd char(including: (lines[1..3]) '{string.Join(',', lines[1..3])}'");
        Console.WriteLine($"Take first 2 elements: (lines[..2]) '{string.Join(',', lines[..2])}'");
        
        Console.WriteLine($"Take last element: (lines[^1]) '{string.Join(',', lines[^1])}'");
        Console.WriteLine($"Take 3rd element from end: (lines[^3]) '{string.Join(',', lines[^3])}'");
        Console.WriteLine($"Take last 2 elements: (lines[^2..]) '{string.Join(',', lines[^2..])}'");
        Console.WriteLine($"Starting from 4 element from the end up to 2nd element from the end : (lines[^4..^1]) '{string.Join(',', lines[^4..^1])}'");
        Console.WriteLine($"From begining up to 2nd element from the end: (lines[..^2]) '{string.Join(',', lines[..^2])}'");
        
    }
}