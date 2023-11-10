using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.Discounts;

public class TransportVoucherDiscount : IDiscount
{
    private const decimal PercentOfDiscountRate = 0.06m;
    private const decimal DiscountFreeCeilingSalary = 1499m;

    public bool IsApplicable(Employee employee)
        => employee.TransportVoucher && employee.GrossSalary > DiscountFreeCeilingSalary;

    public decimal GetDiscount(Employee employee)
        => employee.GrossSalary * PercentOfDiscountRate;

    public string GetDescription()
        => "Vale transporte";
}
