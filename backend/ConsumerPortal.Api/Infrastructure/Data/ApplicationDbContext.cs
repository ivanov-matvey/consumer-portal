using ConsumerPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsumerPortal.Api.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {}

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<Claim> Claims => Set<Claim>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}
