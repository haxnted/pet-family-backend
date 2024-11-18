using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Infrastructure;

public class VolunteerRequestMigrator(VolunteerRequestWriteDbContext context, ILogger<VolunteerRequestMigrator> logger)
    : IMigrator
{
    public async Task Migrate(CancellationToken cancellationToken = default)
    {
        if (await context.Database.CanConnectAsync(cancellationToken) == false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        logger.Log(LogLevel.Information, "Applying volunteer requests migrations...");
        await context.Database.MigrateAsync(cancellationToken);
        logger.Log(LogLevel.Information, "Migrations volunteer requests applied successfully.");
    }
}
