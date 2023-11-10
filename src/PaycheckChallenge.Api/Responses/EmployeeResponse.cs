namespace PaycheckChallenge.Api.Responses;

public record EmployeeResponse
{
    public long Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Document { get; init; }
    public string Sector { get; init; }
    public decimal GrossSalary { get; init; }
    public DateTime AdmissionDate { get; init; }
    public bool DiscountHealthPlan { get; init; }
    public bool DiscountDentalPlane { get; init; }
    public bool TransportVoucher { get; init; }
}
