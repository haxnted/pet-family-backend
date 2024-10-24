using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain;
using PetFamily.Framework.Authorization;

namespace PetFamily.Accounts.Infrastructure.Providers;

public class JwtTokenProvider(IOptions<JwtOptions> options) : ITokenProvider
{
    public string GenerateAccessToken(User user, CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            
            new(CustomClaims.Id, user.Id.ToString()), 
            new (CustomClaims.Email, user.Email ?? ""),
            new (CustomClaims.UserName, user.UserName ?? "")
        ];

        var jwtToken = new JwtSecurityToken(issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(options.Value.ExpirationInMinutes)),
            signingCredentials: signingCredentials,
            claims: claims);
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return token;
    }
}
