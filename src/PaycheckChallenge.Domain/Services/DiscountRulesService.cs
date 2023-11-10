using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Domain.ValueObjects;

namespace PaycheckChallenge.Domain.Services;
public class DiscountRulesService : IDiscountRulesService
{
    private const decimal AmountMax = 99999999.99m;
    private const decimal AmountMin = 1m;

    public IRules GetRuleThatSatisfiesCondition(decimal amount, IEnumerable<IRules> rules)
    {
        foreach (var rule in rules)
        {
            if (rule.IsSatisfiedBy(amount))
            {
                return rule;
            }
        }

        return null;
    }

    public List<IrrfRule> GetIrrfRules()
    {
        return new()
        {
            new(1903.90m, 2826.65m, 0.075m, 142.8m),
            new(2826.66m, 3751.05m, 0.15m, 354.8m),
            new(3751.06m, 4664.68m, 0.225m, 636.13m),
            new(4664.68m, AmountMax, 0.275m, 869.36m),
        };
    }

    public List<InssRule> GetInssRules()
    {
        return new()
        {
            new(AmountMin, 1045m, 0.075m),
            new(1045.01m, 2089.6m, 0.09m),
            new(2089.61m, 3134.4m, 0.12m),
            new(3134.41m, AmountMax, 0.075m),
        };
    }
}
