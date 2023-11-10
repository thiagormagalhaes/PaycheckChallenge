using PaycheckChallenge.Domain.Enums;

namespace PaycheckChallenge.Domain.Dto;
public class TransactionDto
{
    public long PaycheckId;
    public TransactionType Type;
    public decimal Amount;
    public string Description;
}
