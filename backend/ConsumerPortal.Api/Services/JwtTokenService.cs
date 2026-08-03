using ConsumerPortal.Api.Contracts.Auth;
using ConsumerPortal.Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SecurityClaim = System.Security.Claims.Claim;

namespace ConsumerPortal.Api.Services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration) => _configuration = configuration;

    public AuthResponse Create(User user)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var expiresAt = DateTimeOffset.UtcNow.AddHours(8);
        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSection["Key"]!)
        );
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new SecurityClaim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new SecurityClaim(ClaimTypes.Role, user.Role.Name),
            new SecurityClaim(ClaimTypes.Email, user.Email)
        };
        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials
        );

        return new AuthResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            expiresAt,
            new AuthUserDto(user.Id, user.FullName, user.Email, user.Role.Name)
        );
    }
}
