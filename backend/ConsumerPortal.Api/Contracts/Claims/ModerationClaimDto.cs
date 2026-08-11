namespace ConsumerPortal.Api.Contracts.Claims;

public record ModerationClaimDto(
    Guid Id,
    string Title,
    string Text,
    int Status,
    DateTimeOffset CreatedAt,
    Guid CompanyId,
    string CompanyName,
    Guid UserId,
    string UserFullName,
    string UserEmail
);
