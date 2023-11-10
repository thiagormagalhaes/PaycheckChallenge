using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Domain.ValueObjects;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Discounts;
public class InssDiscountTests
{
    private readonly InssDiscount _discount;
    private readonly IDiscountRulesService _discountRulesService;

    public InssDiscountTests()
    {
        _discountRulesService = Substitute.For<IDiscountRulesService>();

        _discount = new InssDiscount(_discountRulesService);
    }

    [Fact]
    public void Should_return_discount_when_rule_exist()
    {
        var employee = new EmployeeBuilder().Build();

        var discountRate = 0.1m;

        var discountExpected = employee.GrossSalary * discountRate;

        var inssRule = new InssRule(1000, 2000, discountRate);

        _discountRulesService.GetRuleThatSatisfiesCondition(Arg.Any<decimal>(), Arg.Any<IEnumerable<IRules>>())
            .Returns(inssRule);

        var result = _discount.GetDiscount(employee);

        Assert.Equal(discountExpected, result);
    }

    [Fact]
    public void Should_return_discount_default_when_rule_not_exist()
    {
        var employee = new EmployeeBuilder().Build();

        _discountRulesService.GetRuleThatSatisfiesCondition(Arg.Any<decimal>(), Arg.Any<IEnumerable<IRules>>())
            .ReturnsNull();

        var result = _discount.GetDiscount(employee);

        Assert.Equal(0, result);
    }

    [Fact]
    public void Should_return_description()
    {
        var result = _discount.GetDescription();

        Assert.Equal("INSS", result);
    }

    [Fact]
    public void Should_return_true_when_discount_is_applicable()
    {
        var employee = new EmployeeBuilder().Build();

        var result = _discount.IsApplicable(employee);

        Assert.True(result);
    }
}
