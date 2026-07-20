using ConsumerPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsumerPortal.Api.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(company => company.Id);

        builder.Property(company => company.Id)
            .ValueGeneratedNever();
        
        builder.Property(company => company.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(company => company.Inn)
            .HasColumnType("varchar(12)")
            .IsRequired();

        builder.HasCheckConstraint(
            "CK_Companies_Inn_Lenght",
            "LEN([Inn]) IN (10, 12)"
        );

        builder.Property(company => company.Category)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(company => company.Inn)
            .IsUnique();
    }
}
