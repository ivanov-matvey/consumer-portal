namespace ConsumerPortal.Api.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
}
