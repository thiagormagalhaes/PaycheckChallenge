namespace PaycheckChallenge.Api.Responses;

public record TransactionResponse
{
    public string Type { get; init; }
    public decimal Amount { get; init; }
    public string Description { get; init; }
}
