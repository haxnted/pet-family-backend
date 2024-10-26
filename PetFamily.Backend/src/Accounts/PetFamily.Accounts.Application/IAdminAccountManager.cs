using PetFamily.Accounts.Domain.TypeAccounts;

namespace PetFamily.Accounts.Application;

public interface IAdminAccountManager
{
    public Task CreateAdminAccountAsync(AdminAccount adminAccount, CancellationToken cancellationToken = default);
}
