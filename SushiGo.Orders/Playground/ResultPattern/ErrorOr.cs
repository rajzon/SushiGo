using Playground.ResultPattern;

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