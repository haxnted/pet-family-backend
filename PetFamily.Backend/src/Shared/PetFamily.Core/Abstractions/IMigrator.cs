namespace PetFamily.Core.Abstractions;

public interface IMigrator
{
    Task Migrate(CancellationToken cancellationToken = default);
}