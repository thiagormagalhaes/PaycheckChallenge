using FluentAssertions;
using NSubstitute;
using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Domain.Services;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Services;
public class DiscountServiceTests
{
    [Fact]
    public void Should_calculate_all_discount_and_return_transactions_when_discount_is_applicable()
    {
        var discounts = new List<IDiscount>
        {
            new FgtsDiscount(),
        };

        var discountService = new DiscountService(discounts);

        var employee = new EmployeeBuilder().Build();

        var result = discountService.CalculateAllDiscount(employee);

        result.Should().HaveCount(1);
    }

    [Fact]
    public void Should_calculate_all_discount_and_not_create_transactions_when_discount_not_is_applicable()
    {
        var discounts = new List<IDiscount>
        {
            new DentalPlanDiscount(),
        };

        var discountService = new DiscountService(discounts);

        var employee = new EmployeeBuilder()
            .WithDiscountDentalPlane(false)
            .Build();

        var result = discountService.CalculateAllDiscount(employee);

        result.Should().BeEmpty();
    }
}
