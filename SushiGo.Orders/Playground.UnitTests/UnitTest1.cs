using FluentAssertions;
using Playground.Entities;
using Playground.Persistance.ValueObjects;

namespace Playground.UnitTests;

public class UnitTest1
{
    [Theory]
    [InlineData("S123456789", RmaType.Shop)]
    [InlineData("C123456789", RmaType.Central)]
    public void Create_ShouldCreateRma(string input, RmaType expectedType)
    {
        var rma = new RmaValue(input);

        rma.Rma.Should().Be(input);
        rma.Type.Should().Be(expectedType);
    }

    [Theory]
    [InlineData("D123456789")]
    [InlineData("1234456789")]
    public void Create_ShouldFailCreateRma_WhenRmaNotValidFormat(string input)
    {
        
        Action act = () => new RmaValue(input);
        
        act.Should().Throw<ArgumentException>().WithMessage($"Invalid rma: {input}");
    }
    
    [Theory]
    [InlineData("S12345678934")]
    [InlineData("C12345678")]
    public void Create_ShouldFailCreateRma_WhenRmaNotValidLength(string input)
    {
        const int length = 10;
        
        Action act = () => new RmaValue(input);
        act.Should().Throw<ArgumentException>().WithMessage($"Invalid length for rma: {input}");
    }
}