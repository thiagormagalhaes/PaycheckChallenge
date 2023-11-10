using MediatR;
using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Application.Queries.GetPaycheck;

public record GetPaycheckQuery(long EmployeeId) : IRequest<Paycheck>;