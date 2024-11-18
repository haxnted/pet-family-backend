using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerManagement.Infrastructure;

public class VolunteerManagementMigrator(VolunteersWriteDbContext context, ILogger<VolunteerManagementMigrator> logger)
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
