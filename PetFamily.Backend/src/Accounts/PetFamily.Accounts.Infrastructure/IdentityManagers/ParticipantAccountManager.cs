using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Accounts.Infrastructure.DbContexts;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class ParticipantAccountManager(AccountsWriteDbContext context) : IParticipantAccountManager
{
    public async Task CreateParticipantAccountAsync(
        ParticipantAccount participantAccount, CancellationToken cancellationToken = default)
    {
        await context.Participants.AddAsync(participantAccount, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
}