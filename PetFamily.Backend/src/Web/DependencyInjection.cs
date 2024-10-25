using Microsoft.OpenApi.Models;
using Serilog;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection collection)
    {
        collection.AddControllers();
        collection.AddEndpointsApiExplorer();
        collection.AddSerilog();
        collection.AddAuthFieldInSwagger();
        collection.AddHttpLogging(u => { u.CombineLogs = true; });
        collection.AddAuthorization();
        return collection;
    }

    private static IServiceCollection AddAuthFieldInSwagger(this IServiceCollection collection)
    {
        return collection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    []
                }
            });
        });
    }
}