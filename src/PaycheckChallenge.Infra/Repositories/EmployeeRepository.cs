using Microsoft.EntityFrameworkCore;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using PaycheckChallenge.Infra.Data;

namespace PaycheckChallenge.Infra.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(PaycheckContext context) : base(context) { }

    public async Task<Employee> GetByDocument(string document)
    {
        return await _set.FirstOrDefaultAsync(x => x.Document == document);
    }
}
