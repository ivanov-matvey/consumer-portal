namespace ConsumerPortal.Api.Contracts.Auth;

public record AuthResponse(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    AuthUserDto User
);

public record AuthUserDto(
    Guid Id,
    string FullName,
    string Email,
    string Role
);
