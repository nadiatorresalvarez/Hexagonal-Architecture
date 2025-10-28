using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Lab09_NadiaTorres.Application.DTOs;
using Lab09_NadiaTorres.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Lab09_NadiaTorres.Infrastructure.Security;
public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;
    public JwtGenerator(IConfiguration configuration) { _configuration = configuration; }

    public string GenerateToken(JwtUserDTO user)
    {
        var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing");
        var issuer = _configuration["Jwt:Issuer"] ?? "lab";
        var audience = _configuration["Jwt:Audience"] ?? "lab";
        var expires = int.TryParse(_configuration["Jwt:ExpiresMinutes"], out var m) ? m : 60;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

        if (user.Roles?.Length > 0) claims.Add(new Claim("roles", JsonConvert.SerializeObject(user.Roles)));

        var keyBytes = Encoding.UTF8.GetBytes(key);
        var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddMinutes(expires), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}