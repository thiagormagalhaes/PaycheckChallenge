namespace PaycheckChallenge.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        Domain.Configurations.DependencyInjection.AddDomain(services);
        Infra.Configurations.DependencyInjection.AddInfra(services);
    }
}
