using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.Accounts.Infrastructure.Seeding;

public class AccountsSeeder(IServiceScopeFactory serviceScopeFactory)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<AccountSeederService>();
        await seeder.SeedAsync(cancellationToken);
    }
}
