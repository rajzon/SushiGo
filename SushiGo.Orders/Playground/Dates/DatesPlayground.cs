namespace Playground.Dates;

internal sealed class DatesPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- DatesPlayground --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        Console.WriteLine("----------------  DatesPlayground.DateTime --------------");
        var currentUtcDate = new DateTime(
            DateTime.UtcNow.Year, 
            DateTime.UtcNow.Month, 
            DateTime.UtcNow.Day, 
            DateTime.UtcNow.Hour, 
            DateTime.UtcNow.Minute, 
            DateTime.UtcNow.Second, 
            kind: DateTimeKind.Utc
        );
        var currentUtcDateOnly = DateOnly.FromDateTime(currentUtcDate);
        Console.WriteLine($"Current UTC date: {currentUtcDate}");
        Console.WriteLine($"Current UTC date only: {currentUtcDateOnly}");

        var originalDate = new DateTime(2025, 1, 31, 0, 0, 0,  kind: DateTimeKind.Utc);
        Console.WriteLine($"Original date UTC: {originalDate}");
        //It will also make sure that if I add 1 month on 31.01 then i make sure that the Ferburuary will be last day for example 28.02.
        Console.WriteLine($"+1 Month: {originalDate.AddMonths(1)}");
        Console.WriteLine($"+1 Month set at hour 23:59:59: {originalDate.AddMonths(1).Date.AddHours(23).AddMinutes(59).AddSeconds(59)}");
        Console.WriteLine($"Date with time 00:00:00: {originalDate.Date}");
        Console.WriteLine($"Day number: {originalDate.Day}");
        Console.WriteLine($"Day number: {originalDate.Month}");
        Console.WriteLine($"Day of week (enum): {originalDate.DayOfWeek}");
        Console.WriteLine($"Day of year: {originalDate.DayOfYear}");
        Console.WriteLine($"Time of day: {originalDate.TimeOfDay}");
        //IMPORTANT ALWAYS SET KIND - if Unspecified then it might work incorectly if compering between Local/UTC. Also it might produce incorect result when Serializing/Saving to EFCore
        Console.WriteLine($"Kind: {originalDate.Kind}");
        var unspecifiedDateTime = new DateTime(2025, 1, 31, 0, 0, 0);
        //It will just create DateTime with same DateTime but with specified kind 
        Console.WriteLine($"Specify Kind Local: {DateTime.SpecifyKind(unspecifiedDateTime, DateTimeKind.Local)}");
        Console.WriteLine($"Specify Kind UTC: {DateTime.SpecifyKind(unspecifiedDateTime, DateTimeKind.Utc)}");
        Console.WriteLine($"Long string version (ex. piątek, 31 stycznia 2025): {originalDate.ToLongDateString()}");
        Console.WriteLine($"Short string version (ex. 31.01.2025): {originalDate.ToShortDateString()}");

        Console.WriteLine("----------------  DatesPlayground.DateTimeOffset --------------");
        var currentDateTimeOffset = DateTimeOffset.Now;
        var currentUtcDateTimeOffset = DateTimeOffset.UtcNow;
        var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        var estTimeFromUtc = TimeZoneInfo.ConvertTime(currentUtcDateTimeOffset, estZone);
        var estTimeFromLocal = TimeZoneInfo.ConvertTime(currentDateTimeOffset, estZone);
        
        Console.WriteLine($"Current UTC DateTimeOffset: {currentUtcDateTimeOffset}");
        Console.WriteLine($"Current UTC DateTimeOffset as current DateTime as Local: {currentUtcDateTimeOffset.LocalDateTime}");
        Console.WriteLine($"Current UTC DateTimeOffset as US Eastern Time: {estTimeFromUtc}");
        //the same will be returned for DateTimeOffset with different zone(ex. Local)
        Console.WriteLine($"Timestamp (Unix Timestamp - number of seconds from 1970year): {currentUtcDateTimeOffset.ToUnixTimeSeconds()}");
        Console.WriteLine($"Timestamp (Unix Timestamp miliseconds - number of miliseconds from 1970year): {currentUtcDateTimeOffset.ToUnixTimeMilliseconds()}");
        
        Console.WriteLine($"Current Local DateTimeOffset: {currentDateTimeOffset}");
        Console.WriteLine($"Current Local DateTimeOffset as current DateTime as UTC: {currentUtcDateTimeOffset.UtcDateTime}");
        Console.WriteLine($"Current Local DateTimeOffset as US Eastern Time: {estTimeFromLocal}");
        Console.WriteLine($"Current Local DateTimeOffset offset minutes: {currentDateTimeOffset.TotalOffsetMinutes}");
        Console.WriteLine($"Current Local DateTimeOffset offset as TimeSpan: {currentDateTimeOffset.Offset}");
        
        // foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
        // {
        //     Console.WriteLine($"Timezone: {tz.Id}");
        // }
        
        Console.WriteLine("---------------- TimeSpan --------------");
        var timeSpan = new TimeSpan(1, 2, 0, 0);
        var secondTimeSpan = new TimeSpan(1, 30, 0);
        Console.WriteLine($"TimeSpan: {timeSpan}");
        Console.WriteLine($"subtraction of TimeSpans: {timeSpan - secondTimeSpan}");
    }
}