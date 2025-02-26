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