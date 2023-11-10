using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Enums;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;

namespace PaycheckChallenge.Domain.Services;

public class DiscountService : IDiscountService
{
    private readonly IEnumerable<IDiscount> _discounts;

    public DiscountService(IEnumerable<IDiscount> discounts)
    {
        _discounts = discounts;
    }

    public List<TransactionDto> CalculateAllDiscount(Employee employee)
    {
        var transactions = new List<TransactionDto>();

        var applicableDiscounts = _discounts.Where(x => x.IsApplicable(employee));

        foreach (var discount in applicableDiscounts)
        {
            transactions.Add(BuildTransactionDto(employee, discount));
        }

        return transactions;
    }

    private TransactionDto BuildTransactionDto(Employee employee, IDiscount discount)
        => new()
        {
            Type = TransactionType.Discount,
            Amount = discount.GetDiscount(employee),
            Description = discount.GetDescription()
        };
}
