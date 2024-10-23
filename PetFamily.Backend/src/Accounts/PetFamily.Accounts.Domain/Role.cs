using Microsoft.AspNetCore.Identity;

namespace PetFamily.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<User> Users { get; init; } = [];
    public List<RolePermission> RolePermissions { get; init; }
}
