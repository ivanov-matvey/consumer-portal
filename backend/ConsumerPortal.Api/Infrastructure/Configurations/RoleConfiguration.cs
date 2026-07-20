using ConsumerPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsumerPortal.Api.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(role => role.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasIndex(role => role.Name)
            .IsUnique();
        
        builder.HasData(
            new Role
            {
                Id = 1,
                Name = "Consumer"
            },
            new Role
            {
                Id = 2,
                Name = "Moderator"
            }
        );
    }
}
