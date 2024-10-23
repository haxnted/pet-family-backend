using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain.TypeAccounts;

public class ParticipantAccount
{
    private ParticipantAccount() { }

    public ParticipantAccount(FullName fullName, User user)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        User = user;
    }
    public Guid Id { get; init; }
    public FullName FullName { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}