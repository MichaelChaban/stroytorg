using Microsoft.IdentityModel.Tokens;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Models.User;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Infrastructure.Configuration.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Stroytorg.Application.Services;

public class TokenGeneratorService(IJwtSettings jwtSettings) : ITokenGeneratorService
{
    private readonly IJwtSettings jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));

    public JwtTokenResponse GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(GetClaims(user)),
            Expires = DateTime.UtcNow.AddHours(jwtSettings.TokenExpiration),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtTokenResponse(Token: tokenHandler.WriteToken(token));
    }

    private IEnumerable<Claim> GetClaims(User context)
    {
        yield return new Claim(ClaimTypes.Name, context.Email);

        yield return new Claim(ClaimTypes.Role, context.ProfileName.ToString());
    }
}
