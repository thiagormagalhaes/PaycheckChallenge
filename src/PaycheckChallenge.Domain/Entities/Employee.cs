namespace PaycheckChallenge.Domain.Entities;

public class Employee : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Document { get; private set; }
    public string Sector { get; private set; }
    public decimal GrossSalary { get; private set; }
    public DateTime AdmissionDate { get; private set; }
    public bool DiscountHealthPlan { get; private set; }
    public bool DiscountDentalPlane { get; private set; }
    public bool TransportVoucher { get; private set; }

    protected Employee() { }

    public Employee(string firstName,
        string lastName,
        string document,
        string sector,
        decimal grossSalary,
        DateTime admissionDate,
        bool discountHealthPlan,
        bool discountdentalPlane,
        bool transportVoucher)
    {
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        Sector = sector;
        GrossSalary = grossSalary;
        AdmissionDate = admissionDate;
        DiscountHealthPlan = discountHealthPlan;
        DiscountDentalPlane = discountdentalPlane;
        TransportVoucher = transportVoucher;
    }
}