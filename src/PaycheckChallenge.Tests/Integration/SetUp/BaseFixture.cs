using Microsoft.Extensions.DependencyInjection;
using PaycheckChallenge.Domain.Entities;
using PaycheckChallenge.Infra.Data;

namespace PaycheckChallenge.Tests.Integration.SetUp;
public class BaseFixture : IDisposable
{
    public CustomWebApplicationFactory<Program> Factory;
    public PaycheckContext Context;
    public HttpClient HttpClient;

    public BaseFixture()
    {
        Factory = new CustomWebApplicationFactory<Program>();
        HttpClient = Factory.CreateClient();
        CreateContxt();
    }

    private void CreateContxt()
    {
        var scope = Factory.Services.CreateScope();
        Context = scope.ServiceProvider.GetRequiredService<PaycheckContext>();
    }

    public int AddEntities(params Entity[] entities)
    {
        Context.AddRange(entities);

        return Context.SaveChanges();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
