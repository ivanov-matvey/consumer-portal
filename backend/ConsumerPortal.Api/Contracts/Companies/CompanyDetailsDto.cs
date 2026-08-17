using ConsumerPortal.Api.Contracts.Claims;
using ConsumerPortal.Api.Contracts.Common;

namespace ConsumerPortal.Api.Contracts.Companies;

public record CompanyDetailsDto(
    Guid Id,
    string Name,
    string Inn,
    int Category,
    PagedResult<ClaimDto> Claims
);
