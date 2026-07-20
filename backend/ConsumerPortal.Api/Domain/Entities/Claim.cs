using ConsumerPortal.Api.Domain.Enums;

namespace ConsumerPortal.Api.Domain.Entities;

public class Claim
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public ClaimStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}
