using Microsoft.AspNetCore.Identity;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public string PhotoPath { get; init; } = string.Empty;
    public List<SocialLink> SocialLinks { get; set; }
    public Guid RoleId { get; init; }
    public Role Role { get; init; } = default!;
    
    public static User CreateAdmin(string userName, string email, Role role)
    {
        return new User
        {
            PhotoPath = "",
            UserName = userName,
            Email = email,
            Role = role
        };
    }
    
    public static User CreateParticipant(string userName, string email, Role role)
    {
        return new User
        {
            PhotoPath = "",
            SocialLinks = [],
            UserName = userName,
            Email = email,
            Role = role
        };
    }
}