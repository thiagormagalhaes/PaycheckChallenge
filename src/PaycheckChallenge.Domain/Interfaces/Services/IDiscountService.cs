using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Domain.Interfaces.Services;

public interface IDiscountService
{
    List<TransactionDto> CalculateAllDiscount(Employee employee);
}
