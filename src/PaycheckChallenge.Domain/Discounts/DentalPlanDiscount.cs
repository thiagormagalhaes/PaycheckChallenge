using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.Discounts;

public class DentalPlanDiscount : IDiscount
{
    private const decimal FlatRate = 5;

    public bool IsApplicable(Employee employee)
        => employee.DiscountDentalPlane;

    public decimal GetDiscount(Employee employee)
        => FlatRate;

    public string GetDescription()
        => "Plano Dental";
}
