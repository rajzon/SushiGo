namespace Playground.Strategy;

internal sealed class PayPalStrategy(string email, string password) : IPaymentStrategy
{
    public Task<bool> Pay(decimal amount)
    {
        Console.WriteLine($"Paying via PayPal. From constructor data: email: {email}, password: {password}");
        return amount is > 0 and < 1000 ?  Task.FromResult(true) : Task.FromResult(false);
    }
}