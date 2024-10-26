namespace PetFamily.SharedKernel;

public interface IHardDeletableService
{
    public Task Clean(CancellationToken cancellationToken);
}
