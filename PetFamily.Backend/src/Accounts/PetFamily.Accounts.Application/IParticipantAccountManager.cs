using PetFamily.Accounts.Domain.TypeAccounts;

namespace PetFamily.Accounts.Application;

public interface IParticipantAccountManager
{
    public Task CreateParticipantAccountAsync(
        ParticipantAccount participantAccount, CancellationToken cancellationToken = default);
}
