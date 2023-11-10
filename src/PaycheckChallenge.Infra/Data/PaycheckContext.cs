using Microsoft.EntityFrameworkCore;

namespace PaycheckChallenge.Infra.Data;

public class PaycheckContext : DbContext
{
    public PaycheckContext(DbContextOptions<PaycheckContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var mappingsAssemply = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(mappingsAssemply);

        base.OnModelCreating(modelBuilder);
    }
}
