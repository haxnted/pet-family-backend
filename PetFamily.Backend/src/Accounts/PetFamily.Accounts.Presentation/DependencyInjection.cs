using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure;

namespace PetFamily.Accounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection collection, IConfiguration configuration)
    {
        return collection
            .AddScoped<IAccountContract, AccountContract>()
            .AddAccountsApplication()
            .AddAccountsInfrastructure(configuration);
    }
}
