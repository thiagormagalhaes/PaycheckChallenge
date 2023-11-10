using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Domain.Interfaces;

public interface IDiscount
{
    bool IsApplicable(Employee employee);
    decimal GetDiscount(Employee employee);
    string GetDescription();
}
