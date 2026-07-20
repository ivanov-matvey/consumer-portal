using ConsumerPortal.Api.Domain.Enums;

namespace ConsumerPortal.Api.Domain.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Inn { get; set; } = null!;
    public CompanyCategory Category { get; set; }
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
}
