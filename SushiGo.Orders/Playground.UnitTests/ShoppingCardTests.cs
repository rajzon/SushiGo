using FluentAssertions;
using NSubstitute;
using Playground.Strategy;

namespace Playground.UnitTests;

public class ShoppingCardAdapterTests
{
    private readonly IPaymentStrategy _paymentStrategy = Substitute.For<IPaymentStrategy>();
    private readonly ShoppingCardAdapter _sut = new ShoppingCardAdapter();

    private void SetupDefaultPayment()
    {
        _paymentStrategy.Pay(Arg.Any<decimal>()).Returns(true);
        _sut.SetPayment(_paymentStrategy);
    }
    
    [Fact]
    public async Task Checkout_ShouldSucceed_WhenPaymentIsSuccessfull()
    {
        SetupDefaultPayment();
        
        var result = await _sut.Checkout(0);
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task Checkout_ShouldFail_WhenPaymentIsNotSuccessfull()
    {
        _paymentStrategy.Pay(Arg.Any<decimal>()).Returns(false);
        _sut.SetPayment(_paymentStrategy);

        var result = await _sut.Checkout(0);
        
        Assert.False(result);
    }
    
    [Fact]
    public async Task Checkout_ShouldFail_WhenAmountIsNegative()
    {
        SetupDefaultPayment();

        var act = async () => await _sut.Checkout(-1);

        await act.Should().ThrowAsync<AmountNegativeException>();
    }
    
    [Fact]
    public async Task Checkout_ShouldFail_WhenPaymentIsNull()
    {
        var act = async () => await _sut.Checkout(0);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}