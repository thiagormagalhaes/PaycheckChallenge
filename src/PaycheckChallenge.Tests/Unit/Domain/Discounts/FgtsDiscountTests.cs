using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Discounts;
public class FgtsDiscountTests
{
    private readonly FgtsDiscount _discount;

    public FgtsDiscountTests()
    {
        _discount = new FgtsDiscount();
    }

    [Fact]
    public void Should_return_discount()
    {
        var employee = new EmployeeBuilder().Build();

        var percentOfDiscountRate = 0.08m;

        var discountExpected = employee.GrossSalary * percentOfDiscountRate;

        var result = _discount.GetDiscount(employee);

        Assert.Equal(discountExpected, result);
    }

    [Fact]
    public void Should_return_description()
    {
        var result = _discount.GetDescription();

        Assert.Equal("FGTS", result);
    }

    [Fact]
    public void Should_return_true_when_discount_is_applicable()
    {
        var employee = new EmployeeBuilder().Build();

        var result = _discount.IsApplicable(employee);

        Assert.True(result);
    }
}
