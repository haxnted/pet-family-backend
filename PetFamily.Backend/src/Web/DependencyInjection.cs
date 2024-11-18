using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using PerFamily.Discussion.Presentation;
using PetFamily.Accounts.Presentation;
using PetFamily.Framework;
using PetFamily.Species.Presentation;
using PetFamily.VolunteerManagement.Presentation;
using PetFamily.VolunteerRequest.Presentation;
using Serilog;

namespace Web;

public static class DependencyInjection
{
    public static void AddModules(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddSpeciesModule();
        collection.AddVolunteerModule(configuration);
        collection.AddAccountsModule(configuration);
        collection.AddVolunteerRequestModule();
        collection.AddDiscussionModule(configuration);
    }

    public static IServiceCollection AddProgramDependency(
        this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddFramework();
        collection.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        collection.AddEndpointsApiExplorer();
        collection.AddSerilog();
        collection.AddAuthFieldInSwagger();
        collection.AddHttpLogging(u =>
        {
            u.CombineLogs = true;
        });
        collection.AddAuthorization();
        collection.AddModules(configuration);
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
