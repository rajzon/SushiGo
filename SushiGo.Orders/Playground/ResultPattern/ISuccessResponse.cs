namespace Playground.ResultPattern;

public interface ISuccessResponse<T>
{
    T Data { get; }
}