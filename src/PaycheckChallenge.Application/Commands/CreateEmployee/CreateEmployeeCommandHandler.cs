using Caelum.Stella.CSharp.Validation;
using MediatR;
using PaycheckChallenge.CrossCutting.Globalization;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using System.ComponentModel;

namespace PaycheckChallenge.Application.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
{
    private const string KeyForInvalidDocument = "InvalidDocument";
    private const string KeyForDocumentIsAlreadyInUse = "DocumentIsAlreadyInUse";

    private readonly ICpfValidator _cpfValidator;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly INotificationContext _notificationContext;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, 
        INotificationContext notificationContext,
        ICpfValidator cpfValidator)
    {
        _cpfValidator = cpfValidator;
        _employeeRepository = employeeRepository;
        _notificationContext = notificationContext;
    }

    public async Task<Employee> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        if (await IsNotValidToCreate(command))
        {
            return null;
        }

        var employee = new Employee(command.FirstName,
            command.LastName,
            command.Document,
            command.Sector,
            command.GrossSalary,
            command.AdmissionDate,
            command.DiscountHealthPlan,
            command.DiscountDentalPlane,
            command.TransportVoucher);

        await _employeeRepository.Add(employee);

        return employee;
    }

    private async Task<bool> IsNotValidToCreate(CreateEmployeeCommand command)
    {
        var validationResult = new CreateEmployeeCommandValidation().Validate(command);

        if (!validationResult.IsValid)
        {
            _notificationContext.AddNotifications(validationResult);
        }

        if (!_cpfValidator.IsValid(command.Document))
        {
            _notificationContext.AddNotification(KeyForInvalidDocument, Resources.InvalidDocument);
        }

        if (await DocumentAlreadyExists(command.Document))
        {
            _notificationContext.AddNotification(KeyForDocumentIsAlreadyInUse, Resources.DocumentIsAlreadyInUse);
        }

        return _notificationContext.HasNotifications();
    }

    private async Task<bool> DocumentAlreadyExists(string document)
    {
        var employee = await _employeeRepository.GetByDocument(document);

        return employee is null ? false : true;
    }
}
