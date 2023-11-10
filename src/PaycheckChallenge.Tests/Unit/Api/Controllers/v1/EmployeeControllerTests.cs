using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PaycheckChallenge.Api.Controllers.v1;
using PaycheckChallenge.Api.Requests;
using PaycheckChallenge.Api.Responses;
using PaycheckChallenge.Application.Commands.CreateEmployee;
using PaycheckChallenge.Application.Queries.GetEmployeeById;
using PaycheckChallenge.Application.Queries.GetPaycheck;
using PaycheckChallenge.CrossCutting.Extensions;
using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Enums;
using PaycheckChallenge.Tests.Builders;
using PaycheckChallenge.Tests.Configurations;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Api.Controllers.v1;
public class EmployeeControllerTests
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly EmployeeController _controller;

    public EmployeeControllerTests()
    {
        _mapper = AutoMapperFixture.GetInstance();
        _mediator = Substitute.For<IMediator>();

        _controller = new EmployeeController(_mapper, _mediator);
    }

    [Fact]
    public async Task Should_call_create_employee_and_return_location_header()
    {
        var employee = new EmployeeBuilder().Build();

        var request = new CreateEmployeeRequest
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Document = employee.Document,
            Sector = employee.Sector,
            GrossSalary = employee.GrossSalary,
            AdmissionDate = employee.AdmissionDate,
            DiscountDentalPlane = employee.DiscountDentalPlane,
            DiscountHealthPlan = employee.DiscountHealthPlan,
            TransportVoucher = employee.TransportVoucher,
        };

        _mediator.Send(Arg.Any<CreateEmployeeCommand>())
            .Returns(employee);

        var locationExpected = $"api/v1/employee/{employee.Id}";

        var result = await _controller.Create(request);

        await AssertSendMediator(request);

        var createdResult = result.Should().BeAssignableTo<CreatedResult>().Subject;
        createdResult.Location.Should().Be(locationExpected);
    }

    private async Task AssertSendMediator(CreateEmployeeRequest request)
    {
        await _mediator.Received(1).Send(Arg.Is<CreateEmployeeCommand>(x =>
               x.FirstName == request.FirstName
            && x.LastName == request.LastName
            && x.Document == request.Document
            && x.Sector == request.Sector
            && x.GrossSalary == request.GrossSalary
            && x.AdmissionDate == request.AdmissionDate
            && x.DiscountDentalPlane == request.DiscountDentalPlane
            && x.DiscountHealthPlan == request.DiscountHealthPlan
            && x.TransportVoucher == request.TransportVoucher
        ));
    }

    [Fact]
    public async Task Should_call_create_employee_and_return_status_ok()
    {
        var employee = new EmployeeBuilder().Build();

        var request = new CreateEmployeeRequest
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Document = employee.Document,
            Sector = employee.Sector,
            GrossSalary = employee.GrossSalary,
            AdmissionDate = employee.AdmissionDate,
            DiscountDentalPlane = employee.DiscountDentalPlane,
            DiscountHealthPlan = employee.DiscountHealthPlan,
            TransportVoucher = employee.TransportVoucher,
        };

        _mediator.Send(Arg.Any<CreateEmployeeCommand>())
            .ReturnsNull();

        var result = await _controller.Create(request);

        await AssertSendMediator(request);

        result.Should().BeAssignableTo<OkResult>();
    }

    [Fact]
    public async Task Should_call_get_employee_id_and_return_status_not_found()
    {
        var employeeId = 1;

        _mediator.Send(Arg.Any<GetEmployeeByIdQuery>())
            .ReturnsNull();

        var result = await _controller.GetById(employeeId);

        await _mediator.Received(1).Send(Arg.Is<GetEmployeeByIdQuery>(x => x.EmployeeId == employeeId));

        result.Should().BeAssignableTo<NotFoundResult>();
    }

    [Fact]
    public async Task Should_call_get_employee_id_and_return_employee()
    {
        var employeeId = 1;

        var employee = new EmployeeBuilder().Build();

        _mediator.Send(Arg.Any<GetEmployeeByIdQuery>())
            .Returns(employee);

        var result = await _controller.GetById(employeeId);

        await _mediator.Received(1).Send(Arg.Is<GetEmployeeByIdQuery>(x => x.EmployeeId == employeeId));

        var okObjectResult = result.Should().BeAssignableTo<OkObjectResult>().Subject;
        var employeeResponse = okObjectResult.Value.Should().BeOfType<EmployeeResponse>().Subject;
        employeeResponse.Id.Should().Be(employee.Id);
        employeeResponse.FirstName.Should().Be(employee.FirstName);
        employeeResponse.LastName.Should().Be(employee.LastName);
        employeeResponse.Document.Should().Be(employee.Document);
        employeeResponse.Sector.Should().Be(employee.Sector);
        employeeResponse.GrossSalary.Should().Be(employee.GrossSalary);
        employeeResponse.AdmissionDate.Should().Be(employee.AdmissionDate);
        employeeResponse.DiscountHealthPlan.Should().Be(employee.DiscountHealthPlan);
        employeeResponse.DiscountDentalPlane.Should().Be(employee.DiscountDentalPlane);
        employeeResponse.TransportVoucher.Should().Be(employee.TransportVoucher);
    }

    [Fact]
    public async Task Should_call_get_paycheck_and_return_paycheckReponse()
    {
        var employeeId = 1;

        var employee = new EmployeeBuilder().Build();

        var compensationTransaction = new TransactionDto
        {
            Type = TransactionType.Compensation,
            Amount = employee.GrossSalary, 
            Description = "Salário"
        };

        var paycheck = new Paycheck(employee);
        paycheck.AddTransactions(compensationTransaction);

        var transactions = paycheck.Transactions.First();

        _mediator.Send(Arg.Any<GetPaycheckQuery>())
            .Returns(paycheck);

        var result = await _controller.GetPaycheck(employeeId);

        await _mediator.Received(1).Send(Arg.Is<GetPaycheckQuery>(x => x.EmployeeId == employeeId));

        var okObjectResult = result.Should().BeAssignableTo<OkObjectResult>().Subject;
        var paycheckResponse = okObjectResult.Value.Should().BeOfType<PaycheckResponse>().Subject;
        paycheckResponse.ReferenceMonth.Should().Be(paycheck.ReferenceMonth);
        paycheckResponse.GrossSalary.Should().Be(paycheck.GrossSalary);
        paycheckResponse.TotalDiscount.Should().Be(paycheck.TotalDiscount);
        paycheckResponse.NetSalary.Should().Be(paycheck.NetSalary);
        paycheckResponse.Transactions.First().Type.Should().Be(EnumExtensions.GetDescriptionFromEnumValue(TransactionType.Compensation));
        paycheckResponse.Transactions.First().Amount.Should().Be(transactions.Amount);
        paycheckResponse.Transactions.First().Description.Should().Be(transactions.Description);
    }
}
