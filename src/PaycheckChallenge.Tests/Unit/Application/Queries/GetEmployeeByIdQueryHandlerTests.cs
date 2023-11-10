using FluentAssertions;
using NSubstitute;
using PaycheckChallenge.Application.Queries.GetEmployeeById;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Application.Queries;
public class GetEmployeeByIdQueryHandlerTests
{
    private readonly IEmployeeRepository _employeeRepository;

    private readonly GetEmployeeByIdQueryHandler _queryHandler;

    public GetEmployeeByIdQueryHandlerTests()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();

        _queryHandler = new GetEmployeeByIdQueryHandler(_employeeRepository);
    }

    [Fact]
    public async Task Should_get_employee_by_id()
    {
        var employee = new EmployeeBuilder().Build();

        var query = new GetEmployeeByIdQuery(employee.Id);

        _employeeRepository.GetById(employee.Id)
            .Returns(employee);

        var result = await _queryHandler.Handle(query, new CancellationToken());

        await _employeeRepository.Received(1).GetById(employee.Id);

        result.Id.Should().Be(employee.Id);
        result.FirstName.Should().Be(employee.FirstName);
        result.LastName.Should().Be(employee.LastName);
        result.Document.Should().Be(employee.Document);
        result.Sector.Should().Be(employee.Sector);
        result.GrossSalary.Should().Be(employee.GrossSalary);
        result.AdmissionDate.Should().Be(employee.AdmissionDate);
        result.DiscountHealthPlan.Should().Be(employee.DiscountHealthPlan);
        result.DiscountDentalPlane.Should().Be(employee.DiscountDentalPlane);
        result.TransportVoucher.Should().Be(employee.TransportVoucher);
    }
}
