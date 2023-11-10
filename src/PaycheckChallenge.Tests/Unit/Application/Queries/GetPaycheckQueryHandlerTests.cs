using FluentAssertions;
using NSubstitute;
using PaycheckChallenge.Application.Queries.GetPaycheck;
using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Application.Queries;
public class GetPaycheckQueryHandlerTests
{
    private readonly IDiscountService _discountService;
    private readonly IEmployeeRepository _employeeRepository;

    public readonly GetPaycheckQueryHandler _queryHandler;

    public GetPaycheckQueryHandlerTests()
    {
        _discountService = Substitute.For<IDiscountService>();
        _employeeRepository = Substitute.For<IEmployeeRepository>();

        _queryHandler = new GetPaycheckQueryHandler(_discountService, _employeeRepository);
    }

    [Fact]
    public async Task Should_get_paycheck_and_return_null_when_not_exist_employee()
    {
        var employeeId = 1;

        var request = new GetPaycheckQuery(employeeId);

        var result = await _queryHandler.Handle(request, CancellationToken.None);

        result.Should().BeNull();
        await _employeeRepository.Received(1).GetById(employeeId);
    }

    [Fact]
    public async Task Should_get_paycheck_when_exist_employee()
    {
        var employee = new EmployeeBuilder().Build();

        var request = new GetPaycheckQuery(employee.Id);

        _employeeRepository.GetById(employee.Id)
            .Returns(employee);

        _discountService.CalculateAllDiscount(employee)
            .Returns(new List<TransactionDto>());

        var result = await _queryHandler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        await _employeeRepository.Received(1).GetById(employee.Id);
        _discountService.Received(1).CalculateAllDiscount(Arg.Is<Employee>(x =>
            x.Id == employee.Id
        ));
    }
}
