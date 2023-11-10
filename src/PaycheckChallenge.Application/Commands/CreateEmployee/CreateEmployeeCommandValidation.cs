using FluentValidation;
using PaycheckChallenge.CrossCutting.Globalization;

namespace PaycheckChallenge.Application.Commands.CreateEmployee;

public class CreateEmployeeCommandValidation : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(Resources.FirstNameIsMandatory)
            .Length(2, 150).WithMessage(Resources.FirstNameHasInvalidLength);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(Resources.LastNameIsMandatory)
            .Length(2, 150).WithMessage(Resources.LastNameHasInvalidLength);

        RuleFor(x => x.Document)
            .Length(11).WithMessage(Resources.DocumentIsMandatory);

        RuleFor(x => x.Sector)
            .NotEmpty().WithMessage(Resources.SectorIsMandatory)
            .Length(2, 150).WithMessage(Resources.SectorHasInvalidLength);

        RuleFor(x => x.GrossSalary)
            .NotEmpty().WithMessage(Resources.GrossSalaryIsMandatory)
            .GreaterThan(0).WithMessage(Resources.InvalidSalary);

        RuleFor(x => x.AdmissionDate)
            .NotEmpty().WithMessage(Resources.AdmissionDateIsMandatory);
    }
}
