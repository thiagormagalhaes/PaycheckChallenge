using Xunit;

namespace PaycheckChallenge.Tests.Integration.SetUp;
public class AbstractIntegrationTests<TFixture> : IClassFixture<TFixture>, IDisposable where TFixture : class
{
    protected TFixture _fixture;

    protected AbstractIntegrationTests(TFixture fixture)
    {
        _fixture = fixture;
    }

    public void Dispose()
    {
        _fixture = null;
        GC.SuppressFinalize(this);
    }
}
