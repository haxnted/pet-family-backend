namespace PetFamily.Accounts.Domain;

public class Permission
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public List<RolePermission> RolePermissions { get; init; } = [];
}
