namespace PaycheckChallenge.Domain.Interfaces;
public interface IRules
{
    bool IsSatisfiedBy(decimal amount);
    decimal GetDiscount(decimal amount);
}
