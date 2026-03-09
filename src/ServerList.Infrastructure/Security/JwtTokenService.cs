using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerList.Application.Abstractions.Security;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Security;


public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _options;

    public JwtTokenService(
        IOptions<JwtOptions> options
    )
    {
        _options = options.Value;
    }

    public string GenerateAccessToken(User user, IReadOnlyCollection<string> roles)
    {
        if (string.IsNullOrWhiteSpace(_options.Key))
        throw new InvalidOperationException("Jwt:Key is not configured.");

        if (string.IsNullOrWhiteSpace(_options.Issuer))
            throw new InvalidOperationException("Jwt:Issuer is not configured.");

        if (string.IsNullOrWhiteSpace(_options.Audience))
            throw new InvalidOperationException("Jwt:Audience is not configured.");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.UserName)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiredAt = DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeMinutes);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expiredAt,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }    

    public DateTime GetAccessTokenExpiryUtc()
    {
        return DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeMinutes);
    }
}