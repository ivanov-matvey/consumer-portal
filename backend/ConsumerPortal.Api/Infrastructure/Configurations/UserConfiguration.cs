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

        builder.HasData(
            new User
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                FullName = "Демонстрационный пользователь",
                Email = "demo@consumer-portal.local",
                PasswordHash = "$2a$11$L6TImdFX/hcvxpnKj2DcW.AsjTQKaOdFtHGQh8DPx7Db/.jWur8LW",
                CreatedAt = new DateTimeOffset(2026, 7, 27, 0, 0, 0, TimeSpan.Zero),
                RoleId = 1
            },
            new User
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                FullName = "Демонстрационный модератор",
                Email = "moderator@consumer-portal.local",
                PasswordHash = "$2a$11$9wdMc/10SfSqXv.HQ8vYN.av0mwDgluA8uQ82Jg7pSlFZBb9NGvFW",
                CreatedAt = new DateTimeOffset(2026, 8, 11, 0, 0, 0, TimeSpan.Zero),
                RoleId = 2
            }
        );
    }
}
