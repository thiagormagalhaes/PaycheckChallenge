using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.Discounts;

public class HealthPlanDiscount : IDiscount
{
    private const decimal FlatRate = 10;

    public bool IsApplicable(Employee employee)
        => employee.DiscountHealthPlan;

    public decimal GetDiscount(Employee employee)
        => FlatRate;

    public string GetDescription()
        => "Plano de Saúde";
}
