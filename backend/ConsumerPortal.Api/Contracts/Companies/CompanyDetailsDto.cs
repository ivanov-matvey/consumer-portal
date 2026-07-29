using ConsumerPortal.Api.Contracts.Claims;

namespace ConsumerPortal.Api.Contracts.Companies;

public record CompanyDetailsDto(
    Guid Id,
    string Name,
    string Inn,
    int Category,
    IReadOnlyCollection<ClaimDto> Claims
);
