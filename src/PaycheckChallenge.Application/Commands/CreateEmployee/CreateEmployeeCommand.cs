using MediatR;
using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Application.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<Employee>
{
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
