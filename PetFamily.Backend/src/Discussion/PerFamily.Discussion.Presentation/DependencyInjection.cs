using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerFamily.Discussion.Application;
using PerFamily.Discussion.Contracts;
using PerFamily.Discussion.Infrastructure;
using PerFamily.Discussion.Presentation.Contracts;

namespace PerFamily.Discussion.Presentation;

public static class DependencyInjection
{
    public static void AddDiscussionModule(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IDiscussionContract, DiscussionContract>()
            .AddDiscussionApplication()
            .AddDiscussionInfrastructure();
    }
}
