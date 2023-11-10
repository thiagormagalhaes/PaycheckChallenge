using PaycheckChallenge.Domain.ValueObjects;

namespace PaycheckChallenge.Domain.Interfaces.Services;
public interface IDiscountRulesService
{
    List<IrrfRule> GetIrrfRules();
    List<InssRule> GetInssRules();
    IRules GetRuleThatSatisfiesCondition(decimal amount, IEnumerable<IRules> rules);
}
