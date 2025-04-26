using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookReview.Infrastructure.Adapters;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateAccessToken(User token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("TokenGeneration:SecretKey").Value ?? ""));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = GetClaims(token.Email, token.FullName, false, token.Id);

        var tokenDescriptor = GetTokenDescriptor(620, credentials, claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private static List<Claim> GetClaims(
       string email, string name, bool hasEntraId, int? userId = 0)
    {

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(ClaimTypes.Name, name),
            new Claim("HasEntraId", hasEntraId.ToString()),
        };

        if (userId != 0)
            claims.Add(new Claim("UserId", userId.ToString() ?? string.Empty));
        return claims;
    }

    private SecurityTokenDescriptor GetTokenDescriptor(int expireMinutes, SigningCredentials credentials, List<Claim> claims)
    {
        return new SecurityTokenDescriptor
        {
            Issuer = _config.GetSection("TokenGeneration:Issuer").Value,
            Audience = _config.GetSection("TokenGeneration:Audience").Value,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims)
        };
    }
}
