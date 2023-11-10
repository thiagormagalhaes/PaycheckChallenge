using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Discounts;
public class HealthPlanDiscountTests
{
    private readonly HealthPlanDiscount _discount;

    public HealthPlanDiscountTests()
    {
        _discount = new HealthPlanDiscount();
    }

    [Fact]
    public void Should_return_discount()
    {
        var employee = new EmployeeBuilder().Build();

        var result = _discount.GetDiscount(employee);

        Assert.Equal(10, result);
    }

    [Fact]
    public void Should_return_description()
    {
        var result = _discount.GetDescription();

        Assert.Equal("Plano de Saúde", result);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Should_return_if_discount_is_applicable(bool isApplicable)
    {
        var employee = new EmployeeBuilder()
            .WithDiscountHealthPlan(isApplicable)
            .Build();

        var result = _discount.IsApplicable(employee);

        Assert.Equal(isApplicable, result);
    }
}
