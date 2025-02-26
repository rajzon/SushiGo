namespace Playground.Strategy;

internal sealed class ShoppingCard
{
    private IPaymentStrategy? _paymentStrategy;
    //Various different things needed for shopping card to work etc.
    
    public void SetPayment(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public async Task<bool> Checkout(decimal amount)
    {
        if (amount < 0)
        {
            throw new AmountNegativeException();
        }
        
        Console.WriteLine($"Running checkout process; {amount}");
        ArgumentNullException.ThrowIfNull(_paymentStrategy);
        var result = await _paymentStrategy.Pay(amount);

        return result;
    }
}