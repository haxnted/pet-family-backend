using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.SharedKernel;


namespace PetFamily.Core.BackgroundServices;

public class EntitiesCleanerBackgroundService(
    ILogger<EntitiesCleanerBackgroundService> logger,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private const int DELAY_HOURS_TO_NEXT_CLEAN = 24;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = scopeFactory.CreateAsyncScope();

            var services = scope.ServiceProvider.GetServices<IHardDeletableService>();
            foreach (var service in services)
            {
                await service.Clean(stoppingToken);
            }
            logger.Log(LogLevel.Information, "Executed EntitiesCleanerBackgroundService");
            
            await Task.Delay(TimeSpan.FromHours(DELAY_HOURS_TO_NEXT_CLEAN), stoppingToken);
        }
    }
}

