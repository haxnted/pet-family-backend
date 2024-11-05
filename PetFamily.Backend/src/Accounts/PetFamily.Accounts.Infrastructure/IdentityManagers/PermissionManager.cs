using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.DbContexts;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class PermissionManager(AccountsWriteDbContext context)
{
    public async Task<Permission?> FindByCodeAsync(string code, CancellationToken cancellationToken = default) =>
        await context.Permissions.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);

    public async Task AddRangeIfExistsAsync(IEnumerable<string> permissions, CancellationToken cancellationToken)
    {
        var uniquePermissions = permissions.ToList().Distinct();
        foreach (var permissionCode in uniquePermissions)
        {
            var isPermissionExists = await context.Permissions
                .AnyAsync(p => p.Code == permissionCode, cancellationToken);

            if (isPermissionExists is false)
                await context.Permissions.AddAsync(new Permission() { Code = permissionCode }, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result<IEnumerable<string>,Error>> GetPermissionsByUserId(
        Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.RolePermissions)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
            return Errors.General.NotFound();

        var permissions = user.Roles
            .SelectMany(r => r.RolePermissions.Select(rp => rp.Permission.Code)).ToList();
        
        return permissions;
    }
}
