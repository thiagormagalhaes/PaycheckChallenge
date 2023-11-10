using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;

namespace PaycheckChallenge.Domain.Discounts;

public class InssDiscount : IDiscount
{
    private readonly IDiscountRulesService _discountRulesService;

    public InssDiscount(IDiscountRulesService discountRulesService)
    {
        _discountRulesService = discountRulesService;
    }

    public bool IsApplicable(Employee employee)
        => true;

    public decimal GetDiscount(Employee employee)
    {
        var inssRules = _discountRulesService.GetInssRules();

        var rule = _discountRulesService.GetRuleThatSatisfiesCondition(employee.GrossSalary,
            inssRules);

        if (rule is null)
        {
            return default;
        }

        return rule.GetDiscount(employee.GrossSalary);
    }

    public string GetDescription()
        => "INSS";
}
