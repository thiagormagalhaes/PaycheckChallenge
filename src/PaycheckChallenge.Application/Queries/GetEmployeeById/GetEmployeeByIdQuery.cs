using MediatR;
using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Application.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(long EmployeeId) : IRequest<Employee>;
