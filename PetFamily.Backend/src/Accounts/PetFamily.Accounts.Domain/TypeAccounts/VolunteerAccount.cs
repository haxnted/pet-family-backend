using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain.TypeAccounts;

public class VolunteerAccount
{
    private VolunteerAccount() { }

    public VolunteerAccount(int experience, List<Requisite> requisites, User user)
    {
        Id = Guid.NewGuid();
        Experience = experience;
        Requisites = requisites;
        UserId = user.Id;
        User = user;
    }

    public Guid Id { get; init; }
    public int Experience { get; init; }
    public List<Requisite> Requisites { get; set; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}
