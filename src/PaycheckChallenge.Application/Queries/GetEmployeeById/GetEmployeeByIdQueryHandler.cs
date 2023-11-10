using MediatR;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces.Repositories;

namespace PaycheckChallenge.Application.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetById(query.EmployeeId);
    }
}
