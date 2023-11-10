using FluentAssertions;
using FluentValidation.Results;
using NSubstitute;
using PaycheckChallenge.Application.Commands.CreateEmployee;
using PaycheckChallenge.CrossCutting.Globalization;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Application.Commands;
public class CreateEmployeeCommandHandlerTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly INotificationContext _notificationContext;
    private readonly ICpfValidator _cpfValidator;

    private readonly CreateEmployeeCommandHandler _commandHandler;

    public CreateEmployeeCommandHandlerTests()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _notificationContext = Substitute.For<INotificationContext>();
        _cpfValidator = Substitute.For<ICpfValidator>();

        _commandHandler = new CreateEmployeeCommandHandler(_employeeRepository, _notificationContext, _cpfValidator);
    }

    [Fact]
    public async Task Should_create_and_return_employee()
    {
        var employee = new EmployeeBuilder().Build();

        _cpfValidator.IsValid(employee.Document)
            .Returns(true);

        var command = BuildCreateEmployeeCommandValid(employee);

        var result = await _commandHandler.Handle(command, CancellationToken.None);

        _notificationContext.DidNotReceive().AddNotification(Arg.Any<string>(), Arg.Any<string>());
        _notificationContext.DidNotReceive().AddNotifications(Arg.Any<ValidationResult>());
        _notificationContext.Received(1).HasNotifications();
        await _employeeRepository.Received(1).Add(AssertEmployee(employee));
        result.Id.Should().Be(employee.Id);
        result.LastName.Should().Be(employee.LastName);
        result.Document.Should().Be(employee.Document);
        result.Sector.Should().Be(employee.Sector);
        result.GrossSalary.Should().Be(employee.GrossSalary);
        result.AdmissionDate.Should().Be(employee.AdmissionDate);
        result.DiscountHealthPlan.Should().Be(employee.DiscountHealthPlan);
        result.DiscountDentalPlane.Should().Be(employee.DiscountDentalPlane);
        result.TransportVoucher.Should().Be(employee.TransportVoucher);
    }

    private CreateEmployeeCommand BuildCreateEmployeeCommandValid(Employee employee)
        => new CreateEmployeeCommand
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Document = employee.Document,
            Sector = employee.Sector,
            GrossSalary = employee.GrossSalary,
            AdmissionDate = employee.AdmissionDate,
            DiscountHealthPlan = employee.DiscountHealthPlan,
            DiscountDentalPlane = employee.DiscountDentalPlane,
            TransportVoucher = employee.TransportVoucher,
        };

    private Employee AssertEmployee(Employee employee)
        => Arg.Is<Employee>(x =>
               x.Id == employee.Id
            && x.FirstName == employee.FirstName
            && x.LastName == employee.LastName
            && x.Document == employee.Document
            && x.Sector == employee.Sector
            && x.GrossSalary == employee.GrossSalary
            && x.AdmissionDate == employee.AdmissionDate
            && x.DiscountHealthPlan == employee.DiscountHealthPlan
            && x.DiscountDentalPlane == employee.DiscountDentalPlane
            && x.TransportVoucher == employee.TransportVoucher
        );

    [Fact]
    public async Task Should_try_create_employee_and_return_null_when_command_is_invalid()
    {
        var employee = new EmployeeBuilder().Build();

        var command = new CreateEmployeeCommand();

        _notificationContext.HasNotifications()
            .Returns(true);

        var result = await _commandHandler.Handle(command, CancellationToken.None);

        _notificationContext.Received(1).AddNotifications(Arg.Any<ValidationResult>());
        _notificationContext.Received(1).HasNotifications();
        await _employeeRepository.DidNotReceive().Add(AssertEmployee(employee));
        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_try_create_employee_and_add_notification_when_document_is_invalid()
    {
        var employee = new EmployeeBuilder().Build();

        var command = new CreateEmployeeCommand();

        _cpfValidator.IsValid(command.Document)
            .Returns(false);

        _notificationContext.HasNotifications()
            .Returns(true);

        var result = await _commandHandler.Handle(command, CancellationToken.None);

        _notificationContext.Received().AddNotification("InvalidDocument", Resources.InvalidDocument);
        _notificationContext.Received(1).HasNotifications();
        await _employeeRepository.DidNotReceive().Add(AssertEmployee(employee));
        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_try_create_employee_and_add_notification_when_document_already_exists()
    {
        var employee = new EmployeeBuilder().Build();

        var command = BuildCreateEmployeeCommandValid(employee);

        _employeeRepository.GetByDocument(command.Document)
            .Returns(employee);

        _notificationContext.HasNotifications()
            .Returns(true);

        var result = await _commandHandler.Handle(command, CancellationToken.None);

        _notificationContext.Received().AddNotification("DocumentIsAlreadyInUse", Resources.DocumentIsAlreadyInUse);
        _notificationContext.Received(1).HasNotifications();
        await _employeeRepository.DidNotReceive().Add(AssertEmployee(employee));
        result.Should().BeNull();
    }
}
