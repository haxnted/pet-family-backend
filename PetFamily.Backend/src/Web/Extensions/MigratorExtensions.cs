using PetFamily.Core.Abstractions;

namespace Web.Extensions;

public static class MigratorExtensions
{
    public static void RunMigrations(this IServiceProvider serviceProvider)
    {
        var migrators = serviceProvider.GetServices<IMigrator>();
        foreach (var migrator in migrators)
        {
            migrator.Migrate();
        }
    }
}
