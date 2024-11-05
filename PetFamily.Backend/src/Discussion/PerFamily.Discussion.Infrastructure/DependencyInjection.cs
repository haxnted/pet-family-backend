using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerFamily.Discussion.Application;
using PerFamily.Discussion.Infrastructure.Repositories;

namespace PerFamily.Discussion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionInfrastructure(this IServiceCollection collection)
    {
        collection.AddScoped<DiscussionWriteDbContext>();
        collection.AddScoped<IDiscussionRepository, DiscussionRepository>();
        collection.AddScoped<IDiscussionReadDbContext, DiscussionReadDbContext>();
        collection.AddScoped<IDiscussionUnitOfWork, DiscussionUnitOfWork>();
        return collection;
    }
}