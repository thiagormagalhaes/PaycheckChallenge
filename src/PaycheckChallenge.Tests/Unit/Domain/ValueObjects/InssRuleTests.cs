using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.ValueObjects;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.ValueObjects;
public class InssRuleTests
{
    private readonly IRules _rule;
    private readonly decimal _discountRate = 0.1m;

    public InssRuleTests()
    {
        _rule = new InssRule(1000, 2000, _discountRate);
    }

    [Fact]
    public void Should_return_discount()
    {
        var amount = 100m;

        var discountExpected = amount * _discountRate;

        var result = _rule.GetDiscount(amount);

        Assert.Equal(discountExpected, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(999.99)]
    [InlineData(2000.01)]
    [InlineData(9999)]
    public void Should_return_false_when_rule_not_is_satisfied(decimal amount)
    {
        var result = _rule.IsSatisfiedBy(amount);

        Assert.False(result);
    }

    [Theory]
    [InlineData(1000)]
    [InlineData(1500)]
    [InlineData(2000)]
    public void Should_return_true_when_rule_is_satisfied(decimal amount)
    {
        var result = _rule.IsSatisfiedBy(amount);

        Assert.True(result);
    }
}
