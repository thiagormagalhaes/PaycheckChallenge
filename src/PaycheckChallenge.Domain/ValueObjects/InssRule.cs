using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.ValueObjects;
public record InssRule(decimal AmountMin, decimal AmountMax, decimal DiscountRate) : IRules
{
    public bool IsSatisfiedBy(decimal amount)
        => amount >= AmountMin && amount <= AmountMax;

    public decimal GetDiscount(decimal amount)
        => amount * DiscountRate;
};
