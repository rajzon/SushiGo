namespace Playground.ResultPattern;

public interface IFailureResponse<T>
{
    string Error { get; }
}