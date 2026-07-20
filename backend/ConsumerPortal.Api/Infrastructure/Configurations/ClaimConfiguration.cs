using ConsumerPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsumerPortal.Api.Infrastructure.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("Claims");

        builder.HasKey(claim => claim.Id);

        builder.Property(claim => claim.Id)
            .ValueGeneratedNever();
        
        builder.Property(claim => claim.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(claim => claim.Text)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(claim => claim.CreatedAt)
            .IsRequired();
        
        builder.HasOne(claim => claim.User)
            .WithMany(user => user.Claims)
            .HasForeignKey(claim => claim.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(claim => claim.Company)
            .WithMany(company => company.Claims)
            .HasForeignKey(claim => claim.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
