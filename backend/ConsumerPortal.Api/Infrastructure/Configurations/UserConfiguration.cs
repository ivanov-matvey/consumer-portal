using ConsumerPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsumerPortal.Api.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever();
        
        builder.Property(user => user.FullName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(user => user.PasswordHash)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.HasIndex(user => user.Email)
            .IsUnique();
        
        builder.HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
