using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;

namespace PaycheckChallenge.Domain.Discounts;

public class IncomeTaxDiscount : IDiscount
{
    private const decimal TaxFreeSalaryCeiling = 1903.89m;

    private readonly IDiscountRulesService _discountRulesService;

    public IncomeTaxDiscount(IDiscountRulesService discountRulesService)
    {
        _discountRulesService = discountRulesService;
    }

    public bool IsApplicable(Employee employee)
        => employee.GrossSalary > TaxFreeSalaryCeiling;

    public decimal GetDiscount(Employee employee)
    {
        var irrfRules = _discountRulesService.GetIrrfRules();

        var rule = _discountRulesService.GetRuleThatSatisfiesCondition(employee.GrossSalary,
            irrfRules);

        if (rule is null)
        {
            return default;
        }

        return rule.GetDiscount(employee.GrossSalary);
    }

    public string GetDescription()
        => "IRRF";
}
