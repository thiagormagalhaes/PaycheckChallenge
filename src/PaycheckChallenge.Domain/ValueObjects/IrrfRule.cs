using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.ValueObjects;
public record IrrfRule(decimal AmountMin, decimal AmountMax, decimal DiscountRate, decimal MaximumDiscountAmount) : IRules
{
    public bool IsSatisfiedBy(decimal amount)
        => amount >= AmountMin && amount <= AmountMax;

    public decimal GetDiscount(decimal amount)
    {
        var discount = amount * DiscountRate;
        return discount > MaximumDiscountAmount ? MaximumDiscountAmount : discount;
    }
};
