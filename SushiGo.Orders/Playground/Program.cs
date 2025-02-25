// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Console.WriteLine(Test.Run());

public interface ISuccessResponse<T>
{
    T Data { get; }
}

public interface IFailureResponse<T>
{
    string Error { get; }
}

public class ErrorOr<T> : ISuccessResponse<T>, IFailureResponse<T>
{
    public bool IsSuccess { get;}
    public T? Data { get; }
    public string? Error { get; }

    private ErrorOr(T data)
    {
        IsSuccess = true;
        Data = data;
    }

    private ErrorOr(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static ErrorOr<T> Success(T data) => new(data);

    public static ErrorOr<T> Failure(string error) => new(error);
}


public static class Test
{
    public static int Run()
    {
        // test.Data = test.Data + 1;
        var result = ErrorOr<int>.Success(10);
        var result2 = ErrorOr<int>.Failure("failure message");

        return result.Data;
    }
}

//Proxy