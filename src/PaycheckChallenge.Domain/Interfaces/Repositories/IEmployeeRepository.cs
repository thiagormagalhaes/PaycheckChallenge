using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Domain.Interfaces.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetByDocument(string document);
}
