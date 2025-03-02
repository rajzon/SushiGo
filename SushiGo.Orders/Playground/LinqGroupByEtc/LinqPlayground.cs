using Azure.Identity;

namespace Playground.LinqGroupByEtc;

public record User(string Name, string Surname, string Group, List<Role> Roles);

public record Role(string Name);

public class SomeClassWithIEquatableAndIComparable : IEquatable<SomeClassWithIEquatableAndIComparable>, IComparable<SomeClassWithIEquatableAndIComparable>
{
    // public int Type { get; set; }
    public string Name { get; set; }
    
    public int CompareTo(SomeClassWithIEquatableAndIComparable other)
    {
        if (other is null)
        {
            return 1;
        }
    
        return Name.CompareTo(other.Name);
    }

    public bool Equals(SomeClassWithIEquatableAndIComparable? other)
    {
        if (other is null)
        {
            return false;
        }

        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

internal sealed class LinqPlayground : IPlayground
{
    private List<Role> roles = [new Role("role1"), new Role("role2")];
    
    public void Run()
    {
        Console.WriteLine("---------------- LinqPlayground --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        int length = 10;
        Console.WriteLine("For loop");
        for (int i = 0; i < length; i++)
        {
            Console.WriteLine($"For loop index {i}");
        }

        Console.WriteLine("Enumerable Range");
        Enumerable.Range(0, 10).ToList().ForEach(index =>
        {
            Console.WriteLine($"Enumerable Range loop index {index}");
        });
        
        
        List<User> users = [
            new User("User 1", "User 1 Surname", "Group1", roles), 
            new User("User 2", "User 2 Surname", "Group1", roles),
            new User("User 3", "User 3 Surname", "Group2", roles),
            new User("User 4", "User 4 Surname", "Group1", roles),
        ];
        
        var query = from user in users
            where user.Group== "Group1"
            select (user.Name, user.Group);
        Console.WriteLine($"LINQ query: {string.Join(',', query.ToList())}");
        
        var chunk = users.Chunk(2).Select((user, index) => $"chunkIndex: {index}. ChunkSize:{user.Length}").ToList();
        chunk.ForEach(Console.WriteLine);
        
        var pageSize = 2;
        var pageNumber = 0;
        var paginationFirstPage = users.Skip(pageSize * pageNumber).Take(pageSize).ToList();
        Console.WriteLine($"LINQ pagination first page: {string.Join(',', paginationFirstPage.Select(user => user.Name))}");
        
        pageNumber = 1;
        var paginationSecondPage = users.Skip(pageSize * pageNumber).Take(pageSize).ToList();
        Console.WriteLine($"LINQ pagination second page: {string.Join(',', paginationSecondPage.Select(user => user.Name))}");

        var takeWhile = users.TakeWhile(user => user.Group == "Group1").Select(user => user.Name).ToList();
        Console.WriteLine($"TakeWhile: {string.Join(',', takeWhile)}");


        Console.WriteLine("---------------Sequence equals -------------");
        var first2UsersNames = users.Take(2).Select(s => s.Name).ToList();
        List<string> twoUsersNames = ["User 1", "User 2"];

        var sequenceEquals = first2UsersNames.SequenceEqual(twoUsersNames);
        Console.WriteLine($"Original List, {string.Join(',', first2UsersNames)}");
        Console.WriteLine($"Second List, {string.Join(',', twoUsersNames)}");
        Console.WriteLine($"Sequence Equals: {sequenceEquals}");

        List<string> twoUsersDifferentOrder = ["User 2", "User 1"];
        
        var sequenceNotEquals = first2UsersNames.SequenceEqual(twoUsersDifferentOrder);
        Console.WriteLine($"Second List, {string.Join(',', twoUsersDifferentOrder)}");
        Console.WriteLine($"Sequence Equals: {sequenceNotEquals}");

        Console.WriteLine("Comparing Record types is also possible");
        var first2Users = users.Take(2).ToList();
        List<User> twoUsers = [
            new User("User 1", "User 1 Surname", "Group1", roles), 
            new User("User 2", "User 2 Surname", "Group1", roles)
        ];
        
        var recordsSequenceEquals = first2Users.SequenceEqual(twoUsers);
        Console.WriteLine($"Original List, {string.Join(',', first2Users.Select(s => s.Name))}");
        Console.WriteLine($"Second List, {string.Join(',', twoUsers.Select(s => s.Name))}");
        Console.WriteLine($"Sequence Equals: {recordsSequenceEquals}");


        Console.WriteLine("Compaing class objects is possible if there is IEquatable interface on them");
        List<SomeClassWithIEquatableAndIComparable> differentArray = [
            new SomeClassWithIEquatableAndIComparable(){Name = "Comparable 1"}, 
            new SomeClassWithIEquatableAndIComparable(){Name = "Comparable 2"}
        ];
        List<SomeClassWithIEquatableAndIComparable> twoObjects = [
            new SomeClassWithIEquatableAndIComparable(){Name = "Comparable 1"}, 
            new SomeClassWithIEquatableAndIComparable(){Name = "Comparable 2"}
        ];
        
        var objectsSequenceEquals = differentArray.SequenceEqual(twoObjects);
        Console.WriteLine($"Original List, {string.Join(',', differentArray.Select(s => s.Name))}");
        Console.WriteLine($"Second List, {string.Join(',', twoObjects.Select(s => s.Name))}");
        Console.WriteLine($"Sequence Equals: {objectsSequenceEquals}");
        
        Console.WriteLine("--------------------------------------------");

        var orderedByObject = twoObjects.OrderDescending();
        Console.WriteLine($"Ordered By object itself thanks to IComparable interface: {string.Join(',', orderedByObject.Select(s => s.Name))}");

        var groups = users.GroupBy(u => u.Group);

        foreach (var group in groups)
        {
            Console.WriteLine($"Group key: {group.Key}");
            group.ToList().ForEach(g => Console.WriteLine($"User name: {g.Name}"));
        }

        Console.WriteLine("IGrouping To Dictionary");
        var groupsToDictionary = groups.ToDictionary(g => g.Key, g => g.ToList());
        foreach (var keyValuePair in groupsToDictionary)
        {
            Console.WriteLine($"KeyValuePairKey: {keyValuePair.Key}");
            keyValuePair.Value.ToList().ForEach(g => Console.WriteLine($"User name: {g.Name}"));
        }
        // var chunks
    }
}