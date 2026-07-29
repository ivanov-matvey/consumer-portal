namespace ConsumerPortal.Api.Contracts.Claims;

public record ClaimDto(
    Guid Id,
    string Title,
    string Text,
    int Status,
    DateTimeOffset CreatedAt,
    Guid CompanyId,
    Guid UserId
);
