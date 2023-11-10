using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Tests.Builders;
public class EmployeeBuilder
{
    private string _firstName = "Thiago";
    private string _lastName = "Magalhães";
    private string _document = "05757253380";
    private string _sector = "TI";
    private decimal _grossSalary = 9000;
    private DateTime _admissionDate = DateTime.UtcNow;
    private bool _discountHealthPlan = true;
    private bool _discountDentalPlane = true;
    private bool _transportVoucher = true;

    public EmployeeBuilder WithDiscountDentalPlane(bool discountDentalPlane)
    {
        _discountDentalPlane = discountDentalPlane;
        return this;
    }

    public EmployeeBuilder WithDiscountHealthPlan(bool discountHealthPlan)
    {
        _discountHealthPlan = discountHealthPlan;
        return this;
    }

    public EmployeeBuilder WithTransportVoucher(bool transportVoucher)
    {
        _transportVoucher = transportVoucher;
        return this;
    }

    public EmployeeBuilder WithGrossSalary(decimal grossSalary)
    {
        _grossSalary = grossSalary;
        return this;
    }

    public Employee Build()
        => new Employee(_firstName,
            _lastName,
            _document,
            _sector,
            _grossSalary,
            _admissionDate,
            _discountHealthPlan,
            _discountDentalPlane,
            _transportVoucher);
}
