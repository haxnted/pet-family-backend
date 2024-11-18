using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;

namespace PerFamily.Discussion.Infrastructure;

public class DiscussionMigrator(DiscussionWriteDbContext context, ILogger<DiscussionMigrator> logger)
    : IMigrator
{
    public async Task Migrate(CancellationToken cancellationToken = default)
    {
        if (await context.Database.CanConnectAsync(cancellationToken) == false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        logger.Log(LogLevel.Information, "Applying volunteers migrations...");
        await context.Database.MigrateAsync(cancellationToken);
        logger.Log(LogLevel.Information, "Migrations volunteers applied successfully.");
    }
}
