namespace Playground.Strategy;

internal sealed  class CreditCardStrategy(string cardNumber, string cvc) : IPaymentStrategy
{
    public Task<bool> Pay(decimal amount)
    {
        Console.WriteLine($"Paying via Credit Card. From constructor data: cardNumber: {cardNumber}, cvc: {cvc}");
        return Task.FromResult(true);
    }
}