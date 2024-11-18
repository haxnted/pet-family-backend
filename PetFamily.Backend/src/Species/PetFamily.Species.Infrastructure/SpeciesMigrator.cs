using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Infrastructure;

public class SpeciesMigrator(SpeciesWriteDbContext context, ILogger<SpeciesMigrator> logger)
    : IMigrator
{
    public async Task Migrate(CancellationToken cancellationToken = default)
    {
        if (await context.Database.CanConnectAsync(cancellationToken) == false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        logger.Log(LogLevel.Information, "Applying species migrations...");
        await context.Database.MigrateAsync(cancellationToken);
        logger.Log(LogLevel.Information, "Migrations species applied successfully.");
    }
}
