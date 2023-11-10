using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaycheckChallenge.Api.Requests;
using PaycheckChallenge.Api.Responses;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Tests.Builders;
using PaycheckChallenge.Tests.Integration.SetUp;
using System.Text;
using Xunit;

namespace PaycheckChallenge.Tests.Integration.Api.Controllers.v1;
public class EmployeeControllerTests : AbstractIntegrationTests<BaseFixture>
{
    public EmployeeControllerTests(BaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task Should_get_employee()
    {
        var employee = new EmployeeBuilder().Build();

        _fixture.AddEntities(employee);

        var response = await _fixture.HttpClient.GetAsync($"api/v1/employee/{employee.Id}");

        response.EnsureSuccessStatusCode();
        
        var result = JsonConvert.DeserializeObject<EmployeeResponse>(await response.Content.ReadAsStringAsync());
        result.Id.Should().Be(employee.Id);
    }

    [Fact]
    public async Task Should_create_employee()
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
            DiscountHealthPlan = employee.DiscountHealthPlan,
            DiscountDentalPlane = employee.DiscountDentalPlane,
            TransportVoucher = employee.TransportVoucher,
        };

        var data = JsonConvert.SerializeObject(request);

        var stringContent = new StringContent(data, Encoding.UTF8, "application/json");

        var response = await _fixture.HttpClient.PostAsync($"api/v1/employee", stringContent);

        response.EnsureSuccessStatusCode();

        response.Headers.Location.Should().Be("api/v1/employee/1");
    }

    [Fact]
    public async Task Should_get_paycheck()
    {
        var employee = new EmployeeBuilder().Build();

        _fixture.AddEntities(employee);

        var response = await _fixture.HttpClient.GetAsync($"api/v1/employee/{employee.Id}/paycheck");

        response.EnsureSuccessStatusCode();

        var result = JsonConvert.DeserializeObject<PaycheckResponse>(await response.Content.ReadAsStringAsync());
        result.GrossSalary.Should().Be(employee.GrossSalary);
        result.Transactions.Should().HaveCountGreaterThan(1);
    }
}
