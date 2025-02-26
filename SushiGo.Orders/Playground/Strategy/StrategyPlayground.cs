using Playground.TaskWhenWaitAll;

namespace Playground.Strategy;

internal sealed class StrategyPlayground : IPlaygroundAsync
{
    public async Task Run()
    {
        Console.WriteLine("---------------- Strategy --------------"); //TODO use Factory/builder or some pattern to put this delimeter for each playgorund
        var shoppingCard = new ShoppingCard();
        shoppingCard.SetPayment(new CreditCardStrategy("123456", "000"));
        
        await shoppingCard.Checkout(100_000m);
        
        shoppingCard.SetPayment(new PayPalStrategy("myemail", "mypassword"));
        await shoppingCard.Checkout(10.5m);
    }
}

public class ShoppingCard
{
    private IPaymentStrategy? _paymentStrategy;
    //Various different things needed for shopping card to work etc.
    
    public void SetPayment(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public async Task<bool> Checkout(decimal amount)
    {
        Console.WriteLine($"Running checkout process; {amount}");
        ArgumentNullException.ThrowIfNull(_paymentStrategy);
        var result = await _paymentStrategy.Pay(amount);

        return result;
    }
}

public interface IPaymentStrategy
{
    Task<bool> Pay(decimal amount);
}

public class PayPalStrategy(string email, string password) : IPaymentStrategy
{
    public Task<bool> Pay(decimal amount)
    {
        Console.WriteLine($"Paying via PayPal. From constructor data: email: {email}, password: {password}");
        return amount is > 0 and < 1000 ?  Task.FromResult(true) : Task.FromResult(false);
    }
}

public class CreditCardStrategy(string cardNumber, string cvc) : IPaymentStrategy
{
    public Task<bool> Pay(decimal amount)
    {
        Console.WriteLine($"Paying via Credit Card. From constructor data: cardNumber: {cardNumber}, cvc: {cvc}");
        return Task.FromResult(true);
    }
}