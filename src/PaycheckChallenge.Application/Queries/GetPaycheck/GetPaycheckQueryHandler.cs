using MediatR;
using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Enums;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using PaycheckChallenge.Domain.Interfaces.Services;

namespace PaycheckChallenge.Application.Queries.GetPaycheck;

public class GetPaycheckQueryHandler : IRequestHandler<GetPaycheckQuery, Paycheck>
{
    private readonly IDiscountService _discountService;
    private readonly IEmployeeRepository _employeeRepository;

    public GetPaycheckQueryHandler(IDiscountService discountService, IEmployeeRepository employeeRepository)
    {
        _discountService = discountService;
        _employeeRepository = employeeRepository;
    }

    public async Task<Paycheck> Handle(GetPaycheckQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetById(request.EmployeeId);

        if (employee is null)
        {
            return null;
        }

        var paycheck = BuildPaycheck(employee);

        return paycheck;
    }

    private Paycheck BuildPaycheck(Employee employee)
    {
        var paycheck = new Paycheck(employee);

        var discountTransactions = _discountService.CalculateAllDiscount(employee)
            .ToArray();

        var compensationTransaction = new TransactionDto
        {
            Type = TransactionType.Compensation, 
            Amount = employee.GrossSalary, 
            Description = "Salário"
        };

        paycheck.AddTransactions(compensationTransaction);
        paycheck.AddTransactions(discountTransactions);

        paycheck.Update();

        return paycheck;
    }
}
