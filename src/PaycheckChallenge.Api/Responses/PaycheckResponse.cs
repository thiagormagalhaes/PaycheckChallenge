namespace PaycheckChallenge.Api.Responses;

public record PaycheckResponse
{
    public int ReferenceMonth { get; init; }
    public List<TransactionResponse> Transactions { get; init; }
    public decimal GrossSalary { get; init; }
    public decimal TotalDiscount { get; init; }
    public decimal NetSalary { get; init; }
}
