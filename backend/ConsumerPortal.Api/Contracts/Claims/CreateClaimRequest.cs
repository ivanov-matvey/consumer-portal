namespace ConsumerPortal.Api.Contracts.Claims;

public record CreateClaimRequest(
    string Title,
    string Text,
    Guid CompanyId,
    string Inn
);
