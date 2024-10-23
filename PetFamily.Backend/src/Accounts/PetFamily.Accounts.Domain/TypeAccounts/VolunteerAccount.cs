using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Domain.TypeAccounts;

public class VolunteerAccount
{
    public Guid Id { get; init; }
    public FullName FullName { get; init; }
    public int Experience { get; init; }
    public List<Requisite> Requisites { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}