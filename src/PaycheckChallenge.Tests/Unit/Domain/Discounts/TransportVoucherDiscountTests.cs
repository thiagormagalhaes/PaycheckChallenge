using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Discounts;
public class TransportVoucherDiscountTests
{
    private readonly TransportVoucherDiscount _discount;

    public TransportVoucherDiscountTests()
    {
        _discount = new TransportVoucherDiscount();
    }

    [Fact]
    public void Should_return_discount()
    {
        var grossSalary = 100;
        var percentOfDiscountRate = 0.06m;

        var employee = new EmployeeBuilder()
            .WithGrossSalary(grossSalary)
            .Build();

        var discountExpected = grossSalary * percentOfDiscountRate;

        var result = _discount.GetDiscount(employee);

        Assert.Equal(discountExpected, result);
    }

    [Fact]
    public void Should_return_description()
    {
        var result = _discount.GetDescription();

        Assert.Equal("Vale transporte", result);
    }

    [Fact]
    public void Should_return_true_when_discount_is_applicable()
    {
        var employee = new EmployeeBuilder().Build();

        var result = _discount.IsApplicable(employee);

        Assert.True(result);
    }

    [Fact]
    public void Should_return_false_when_transport_voucher_is_false()
    {
        var employee = new EmployeeBuilder()
            .WithTransportVoucher(false)
            .Build();

        var result = _discount.IsApplicable(employee);

        Assert.False(result);
    }

    [Fact]
    public void Should_return_false_when_transport_voucher_is_true_and_gross_salary_not_is_discounted()
    {
        var employee = new EmployeeBuilder()
            .WithTransportVoucher(true)
            .WithGrossSalary(1000)
            .Build();

        var result = _discount.IsApplicable(employee);

        Assert.False(result);
    }
}
