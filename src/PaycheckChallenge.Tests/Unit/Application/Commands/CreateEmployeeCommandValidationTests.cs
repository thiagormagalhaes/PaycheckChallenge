using FluentAssertions;
using PaycheckChallenge.Application.Commands.CreateEmployee;
using PaycheckChallenge.CrossCutting.Globalization;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Application.Commands;
public class CreateEmployeeCommandValidationTests
{
    [Fact]
    public void Should_validate_command_and_return_true()
    {
        var employee = new EmployeeBuilder().Build();

        var command = new CreateEmployeeCommand
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

        var validationResult = new CreateEmployeeCommandValidation().Validate(command);

        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_validate_command_and_return_false_when_first_name_is_empty()
    {
        var employee = new EmployeeBuilder().Build();

        var command = new CreateEmployeeCommand
        {
            LastName = employee.LastName,
            Document = employee.Document,
            Sector = employee.Sector,
            GrossSalary = employee.GrossSalary,
            AdmissionDate = employee.AdmissionDate,
            DiscountHealthPlan = employee.DiscountHealthPlan,
            DiscountDentalPlane = employee.DiscountDentalPlane,
            TransportVoucher = employee.TransportVoucher,
        };

        var validationResult = new CreateEmployeeCommandValidation().Validate(command);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.First().ErrorCode.Should().Be("NotEmptyValidator");
        validationResult.Errors.First().ErrorMessage.Should().Be(Resources.FirstNameIsMandatory);
    }
}
