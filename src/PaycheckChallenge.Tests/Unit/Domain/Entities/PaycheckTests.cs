using FluentAssertions;
using PaycheckChallenge.Domain.Dto;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Enums;
using PaycheckChallenge.Tests.Builders;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Entities;
public class PaycheckTests
{
    [Fact]
    public void Should_create_paycheck()
    {
        var employee = new EmployeeBuilder().Build();

        var referenceMonthExpected = DateTime.UtcNow.AddMonths(-1).Month;

        var paycheck = new Paycheck(employee);

        paycheck.EmployeeId.Should().Be(employee.Id);
        paycheck.ReferenceMonth.Should().Be(referenceMonthExpected);
        paycheck.Transactions.Should().BeEmpty();
    }

    [Fact]
    public void Should_add_transactions()
    {
        var employee = new EmployeeBuilder().Build();

        var paycheck = new Paycheck(employee);

        var compensationTransaction = new TransactionDto
        {
            Type = TransactionType.Compensation, 
            Amount = employee.GrossSalary, 
            Description = "Salário"
        };

        paycheck.AddTransactions(compensationTransaction);

        paycheck.Transactions.Should().HaveCount(1);
        paycheck.Transactions.First().Type.Should().Be(compensationTransaction.Type);
        paycheck.Transactions.First().Amount.Should().Be(compensationTransaction.Amount);
        paycheck.Transactions.First().Description.Should().Be(compensationTransaction.Description);
    }

    [Fact]
    public void Should_update_paycheck_attributes()
    {
        var employee = new EmployeeBuilder().Build();

        var paycheck = new Paycheck(employee);

        var compensationTransaction = new TransactionDto
        {
            Type = TransactionType.Compensation,
            Amount = employee.GrossSalary,
            Description = "Salário"
        };

        var discountTransaction = new TransactionDto
        {
            Type = TransactionType.Discount,
            Amount = employee.GrossSalary / 2,
            Description = "Empréstimo"
        };

        var netSalaryExpected = compensationTransaction.Amount - discountTransaction.Amount;

        paycheck.AddTransactions(compensationTransaction, discountTransaction);

        paycheck.Update();

        paycheck.Transactions.Should().HaveCount(2);
        paycheck.GrossSalary.Should().Be(employee.GrossSalary);
        paycheck.NetSalary.Should().Be(netSalaryExpected);
        paycheck.TotalDiscount.Should().Be(-discountTransaction.Amount);
    }
}
