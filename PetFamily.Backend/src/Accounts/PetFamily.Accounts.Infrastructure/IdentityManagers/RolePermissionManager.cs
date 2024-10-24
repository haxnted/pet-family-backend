using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class RolePermissionManager(AccountsDbContext accountsDbContext)
{
    public async Task AddRangeIfExistAsync(
        Guid roleId, IEnumerable<string> permissions, CancellationToken cancellationToken = default)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == permissionCode,
                cancellationToken: cancellationToken);

            if (permission is null)
                throw new ArgumentNullException(nameof(roleId));

            var isPermissionRoleExist = await accountsDbContext.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id,
                    cancellationToken);

            if (isPermissionRoleExist) continue;
            var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permission!.Id };
            await accountsDbContext.RolePermissions.AddAsync(rolePermission, cancellationToken);
        }

        await accountsDbContext.SaveChangesAsync(cancellationToken);
    }
}
