namespace PetFamily.Accounts.Domain;

public class Permission
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public IEnumerable<RolePermission> RolePermissions { get; init; }
}
