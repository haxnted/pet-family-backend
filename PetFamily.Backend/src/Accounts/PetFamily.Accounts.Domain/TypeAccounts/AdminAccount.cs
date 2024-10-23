using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain.TypeAccounts;

public class AdminAccount
{
    private AdminAccount() { }
    
    public AdminAccount(FullName fullName, User user)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        User = user;
        UserId = user.Id;
    }
    public Guid Id { get; init; }
    public FullName FullName { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}