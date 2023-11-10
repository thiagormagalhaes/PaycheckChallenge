using NSubstitute;
using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.ValueObjects;
using PaycheckChallenge.Tests.Builders;
using Xunit;
using NSubstitute.ReturnsExtensions;

namespace PaycheckChallenge.Tests.Unit.Domain.Discounts;
public class IncomeTaxDiscountTests
{
    private readonly IncomeTaxDiscount _discount;
    private readonly IDiscountRulesService _discountRulesService;

    public IncomeTaxDiscountTests()
    {
        _discountRulesService = Substitute.For<IDiscountRulesService>();

        _discount = new IncomeTaxDiscount(_discountRulesService);
    }

    [Fact]
    public void Should_return_discount()
    {
        var employee = new EmployeeBuilder().Build();

        var discountRate = 0.1m;

        var discountExpected = employee.GrossSalary * discountRate;

        var irrfRule = new IrrfRule(2000, 3000, discountRate, 1000);

        _discountRulesService.GetRuleThatSatisfiesCondition(Arg.Any<decimal>(), Arg.Any<IEnumerable<IRules>>())
            .Returns(irrfRule);

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

        Assert.Equal("IRRF", result);
    }

    [Fact]
    public void Should_return_true_when_discount_is_applicable()
    {
        var employee = new EmployeeBuilder().Build();

        var result = _discount.IsApplicable(employee);

        Assert.True(result);
    }

    [Fact]
    public void Should_return_false_when_discount_not_is_applicable()
    {
        var employee = new EmployeeBuilder()
            .WithGrossSalary(1000)
            .Build();

        var result = _discount.IsApplicable(employee);

        Assert.False(result);
    }
}
