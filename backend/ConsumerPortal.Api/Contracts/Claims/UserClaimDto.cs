namespace ConsumerPortal.Api.Contracts.Claims;

public record UserClaimDto(
    Guid Id,
    string Title,
    string Text,
    int Status,
    DateTimeOffset CreatedAt,
    Guid CompanyId,
    string CompanyName
);
