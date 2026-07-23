using ConsumerPortal.Api.Domain.Entities;
using ConsumerPortal.Api.Domain.Enums;
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

        builder.HasData(
            new Company { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "Городская управляющая компания", Inn = "7701234567", Category = CompanyCategory.HousingUtilities },
            new Company { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "Комфортный дом", Inn = "7812345678", Category = CompanyCategory.HousingUtilities },
            new Company { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "Магазин У дома", Inn = "770123456789", Category = CompanyCategory.Retail },
            new Company { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Name = "Торговая сеть Север", Inn = "780123456789", Category = CompanyCategory.Retail },
            new Company { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Name = "Быстрая связь", Inn = "7712345678", Category = CompanyCategory.Telecom },
            new Company { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Name = "Телеком Регион", Inn = "781234567890", Category = CompanyCategory.Telecom }
        );
    }
}
