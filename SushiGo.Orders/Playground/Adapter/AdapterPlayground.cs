namespace Playground.Adapter;

internal sealed class AdapterPlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- AdapterPlayground --------------");
        var legacyShoppingCard = new LegacyShoppingCard(20.23d, 2);
        IShoppingCard shoppingCard = new ShoppingCardAdapter(legacyShoppingCard);
        
        AddPriceToCard(5.22m, shoppingCard);
        DisplayCardPrice(shoppingCard);
    }

    public void DisplayCardPrice(IShoppingCard shoppingCard)
    {
        Console.WriteLine($"CardPrice: {shoppingCard.GetTotalPrice()}");
    }
    public void AddPriceToCard(decimal price, IShoppingCard shoppingCard)
    {
        shoppingCard.AddPrice(price);
    }
}

public interface IShoppingCard
{
    decimal GetTotalPrice();
    void AddPrice(decimal price);
}

public class ShoppingCardAdapter(LegacyShoppingCard legacyCard) : IShoppingCard 
{
    public decimal GetTotalPrice()
    {
        return Convert.ToDecimal(legacyCard.GetCumulativeSum());
    }

    public void AddPrice(decimal price)
    {
        legacyCard.AddPriceToCard(Convert.ToDouble(price));
    }
}


public class LegacyShoppingCard(double price, int itemsCount)
{
    private double _price = price;

    public double GetCumulativeSum()
    {
        return itemsCount * _price;
    }

    public void AddPriceToCard(double price)
    {
        _price += price;
    }
}



