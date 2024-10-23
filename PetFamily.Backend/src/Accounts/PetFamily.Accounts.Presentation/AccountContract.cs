using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure.IdentityManagers;

namespace PetFamily.Accounts.Presentation;

public class AccountContract(PermissionManager permissionManager) : IAccountContract
{
    public async Task<IEnumerable<string>?> GetPermissionsUserById(
        Guid userId, CancellationToken cancellationToken = default)
    {
        return await permissionManager.GetPermissionsByUserId(userId, cancellationToken);
    }
}
