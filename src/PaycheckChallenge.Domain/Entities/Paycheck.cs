using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Enums;

namespace PaycheckChallenge.Domain.Entities;

public class Paycheck : Entity
{
    public long EmployeeId { get; private set; }
    public int ReferenceMonth { get; private set; }
    public decimal GrossSalary { get; private set; } = 0;
    public decimal TotalDiscount { get; private set; } = 0;
    public decimal NetSalary { get; private set; }
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    protected Paycheck() { }

    public Paycheck(Employee employee)
    {
        EmployeeId = employee.Id;
        ReferenceMonth = DateTime.UtcNow.AddMonths(-1).Month;
    }

    public void AddTransactions(params TransactionDto[] transactionsDto)
    {
        _transactions.AddRange(BuildTransactions(transactionsDto));
    }

    private IEnumerable<Transaction> BuildTransactions(params TransactionDto[] transactionsDto)
        => transactionsDto.Select(x => new Transaction(EmployeeId, x));

    public void Update()
    {
        GrossSalary = GetTotalCompensation();
        TotalDiscount = -GetTotalDiscount();
        NetSalary = GrossSalary - Math.Abs(TotalDiscount);
    }

    private decimal GetTotalCompensation()
        => GetCompensationTransactions().Sum(x => x.Amount);

    private IEnumerable<Transaction> GetCompensationTransactions()
        => Transactions.Where(x => x.Type == TransactionType.Compensation);

    private decimal GetTotalDiscount()
        => GetDiscountTransactions().Sum(x => x.Amount);

    private IEnumerable<Transaction> GetDiscountTransactions()
        => Transactions.Where(x => x.Type == TransactionType.Discount);
}
