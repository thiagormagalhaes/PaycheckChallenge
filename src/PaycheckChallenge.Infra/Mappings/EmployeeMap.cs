using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Infra.Mappings;

public class EmployeeMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Document)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.Sector)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.GrossSalary)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.AdmissionDate)
            .IsRequired();

        builder.Property(x => x.DiscountHealthPlan);

        builder.Property(x => x.DiscountDentalPlane);

        builder.Property(x => x.TransportVoucher);
    }
}
