using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PetFamily.Framework.Authorization;

public class UserInfoRequest
{
    public Guid Id { get; }
    public string UserName { get; }

    public UserInfoRequest(IHttpContextAccessor accessor)
    {
        if (accessor.HttpContext?.User?.Claims == null)
            throw new InvalidOperationException("HttpContext or User claims are not available.");

        var userClaims = accessor.HttpContext.User.Claims.ToArray();

        Id = ExtractGuidClaim(userClaims, CustomClaims.Id);
        UserName = ExtractClaimValue(userClaims, CustomClaims.UserName) ?? string.Empty;
    }
    private static Guid ExtractGuidClaim(IEnumerable<Claim> claims, string claimType)

    {
        var claimValue = claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        if (Guid.TryParse(claimValue, out var guid))
            return guid;

        throw new InvalidOperationException($"Claim '{claimType}' is not a valid GUID.");
    }

    private static string? ExtractClaimValue(IEnumerable<Claim> claims, string claimType) =>
        claims.FirstOrDefault(c => c.Type == claimType)?.Value;
}