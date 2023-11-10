using Microsoft.Extensions.DependencyInjection;
using PaycheckChallenge.Domain.Interfaces.Repositories;
using PaycheckChallenge.Infra.Repositories;

namespace PaycheckChallenge.Infra.Configurations;

public static class DependencyInjection
{
    public static void AddInfra(IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}
