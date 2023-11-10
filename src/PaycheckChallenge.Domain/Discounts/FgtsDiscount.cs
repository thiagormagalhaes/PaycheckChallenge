using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.Discounts;

public class FgtsDiscount : IDiscount
{
    private const decimal PercentOfDiscountRate = 0.08m;

    public bool IsApplicable(Employee employee)
        => true;

    public decimal GetDiscount(Employee employee)
        => employee.GrossSalary * PercentOfDiscountRate;

    public string GetDescription()
        => "FGTS";
}
