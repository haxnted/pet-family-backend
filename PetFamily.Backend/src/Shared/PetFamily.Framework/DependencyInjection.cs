using Microsoft.Extensions.DependencyInjection;
using PetFamily.Framework.Authorization;

namespace PetFamily.Framework;

public static class DependencyInjection
{
    public static IServiceCollection AddFramework(this IServiceCollection collection)
    {
        return collection.AddHttpContextAccessor()
            .AddScoped<UserInfoRequest>();
    }
}
