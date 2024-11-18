using Microsoft.AspNetCore.Identity;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public string PhotoPath { get; init; } = string.Empty;
    public FullName FullName { get; set; } = default!;
    public List<SocialLink> SocialLinks { get; set; } 
    private List<Role> _roles = [];
    public IReadOnlyList<Role> Roles => _roles;
    public static User CreateAdmin(FullName fullName, string userName, string email, Role role)
    {
        return new User
        {
            PhotoPath = "",
            FullName = fullName,
            SocialLinks = [],
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }
    
    public static User CreateParticipant(FullName fullName, string userName, string email, Role role)
    {
        return new User
        {
            PhotoPath = "",
            FullName = fullName,
            SocialLinks = [],
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }
}