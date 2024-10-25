﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PetFamily.Accounts.Infrastructure;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters CreateTokenValidation(JwtOptions jwtOptions, bool validateLifetime) =>
        new()
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = validateLifetime,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };

    public static TokenValidationParameters CreateWithoutLifetime(JwtOptions jwtOptions) =>
         CreateTokenValidation(jwtOptions, false);
    

    public static TokenValidationParameters CreateWithLifetime(JwtOptions jwtOptions) =>
        CreateTokenValidation(jwtOptions, true);
}
