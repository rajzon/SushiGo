namespace Playground.Strategy;

public interface IPaymentStrategy
{
    Task<bool> Pay(decimal amount);
}