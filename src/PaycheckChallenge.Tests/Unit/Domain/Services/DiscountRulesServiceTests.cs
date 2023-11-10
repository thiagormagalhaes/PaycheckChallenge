using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Domain.Services;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Services;
public class DiscountRulesServiceTests
{
    private readonly IDiscountRulesService _discountRules;

    public DiscountRulesServiceTests()
    {
        _discountRules = new DiscountRulesService();
    }

    [Fact]
    public void Should_get_irrf_rules()
    {
        var result = _discountRules.GetIrrfRules();

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Should_get_inss_rules()
    {
        var result = _discountRules.GetInssRules();

        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void Should_get_rule_when_satisfies_condition()
    {
        var irrfRules = _discountRules.GetIrrfRules();

        var result = _discountRules.GetRuleThatSatisfiesCondition(2000, irrfRules);

        Assert.NotNull(result);
    }

    [Fact]
    public void Should_get_null_when_not_satisfies_condition()
    {
        var irrfRules = _discountRules.GetIrrfRules();

        var result = _discountRules.GetRuleThatSatisfiesCondition(1, irrfRules);

        Assert.Null(result);
    }
}
