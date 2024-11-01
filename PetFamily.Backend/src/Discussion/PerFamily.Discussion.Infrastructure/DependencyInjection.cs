using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerFamily.Discussion.Application;

namespace PerFamily.Discussion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionInfrastructure(this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddScoped<DiscussionWriteDbContext>();
        collection.AddScoped<IDiscussionUnitOfWork, DiscussionUnitOfWork>();
        return collection;
    }
}