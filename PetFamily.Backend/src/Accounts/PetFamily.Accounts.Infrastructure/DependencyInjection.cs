using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Accounts.Infrastructure.Providers;
using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Framework.Authorization;

namespace PetFamily.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection collection, IConfiguration configuration)
    {
        collection.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
        collection.AddEnv(configuration);
        collection.AddTransient<ITokenProvider, JwtTokenProvider>();
        collection.AddSingleton<AccountsSeeder>();
        collection.AddIdentity();
        collection.AddScoped<RolePermissionManager>();
        collection.AddScoped<AccountsDbContext>();
        collection.AddScoped<PermissionManager>();
        collection.AddScoped<AccountSeederService>();
        collection.AddScoped<AdminAccountManager>();
        collection.AddScoped<IAccountsUnitOfWork, AccountsUnitOfWork>();
        collection.AddScoped<IParticipantAccountManager, ParticipantAccountManager>();
        collection.AddScoped<IVolunteerAccountManager, VolunteerAccountManager>();
        collection.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        collection.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        return collection;
    }

    private static void AddIdentity(this IServiceCollection collection)
    {
        collection.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<AccountsDbContext>();
    }

    private static void AddEnv(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.Configure<AdminOptions>(configuration.GetSection(AdminOptions.ADMIN));
    }
}
