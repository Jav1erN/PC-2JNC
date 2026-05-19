using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PC_2JNC.Application.DTOs.Auth;
using PC_2JNC.Application.Interfaces;

namespace PC_2JNC.Infrastructure.Authentication;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public LoginResponseDto? GenerateToken(LoginRequestDto request)
    {
        if (!IsValidUser(request))
        {
            return null;
        }

        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, request.Username),
            new(JwtRegisteredClaimNames.UniqueName, request.Username),
            new(ClaimTypes.Name, request.Username),
            new(ClaimTypes.Role, "User")
        };

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new LoginResponseDto(
            new JwtSecurityTokenHandler().WriteToken(token),
            "Bearer",
            expiresAt);
    }

    private bool IsValidUser(LoginRequestDto request)
    {
        return string.Equals(request.Username, _settings.DemoUsername, StringComparison.Ordinal)
            && string.Equals(request.Password, _settings.DemoPassword, StringComparison.Ordinal);
    }
}
