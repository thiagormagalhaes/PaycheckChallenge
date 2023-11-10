using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Enums;

namespace PaycheckChallenge.Domain.Entities;

public class Transaction : Entity
{
    public long PaycheckId { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    protected Transaction() { }

    public Transaction(long paycheckId, TransactionDto transactionDto)
    {
        PaycheckId = paycheckId;
        Type = transactionDto.Type;
        Amount = transactionDto.Amount;
        Description = transactionDto.Description;
    }
}
