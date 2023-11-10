using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.ValueObjects;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.ValueObjects;
public class IrrfRuleTests
{
    private readonly IRules _rule;
    private readonly decimal _discountRate = 0.1m;
    private readonly decimal _maximumDiscountAmount = 200m;

    public IrrfRuleTests()
    {
        _rule = new IrrfRule(1000, 3000, _discountRate, _maximumDiscountAmount);
    }

    [Fact]
    public void Should_return_discount()
    {
        var amount = 100m;

        var discountExpected = amount * _discountRate;

        var result = _rule.GetDiscount(amount);

        Assert.Equal(discountExpected, result);
    }

    [Fact]
    public void Should_return_maximum_discount_when_discount_is_greater()
    {
        var amount = 3000m;

        var result = _rule.GetDiscount(amount);

        Assert.Equal(_maximumDiscountAmount, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(999.99)]
    [InlineData(3000.01)]
    [InlineData(9999)]
    public void Should_return_false_when_rule_not_is_satisfied(decimal amount)
    {
        var result = _rule.IsSatisfiedBy(amount);

        Assert.False(result);
    }

    [Theory]
    [InlineData(1000)]
    [InlineData(2000)]
    [InlineData(3000)]
    public void Should_return_true_when_rule_is_satisfied(decimal amount)
    {
        var result = _rule.IsSatisfiedBy(amount);

        Assert.True(result);
    }
}
