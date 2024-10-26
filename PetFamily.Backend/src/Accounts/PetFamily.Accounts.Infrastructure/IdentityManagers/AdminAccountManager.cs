using PetFamily.Accounts.Domain.TypeAccounts;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class AdminAccountManager(AccountsWriteDbContext context) 
{
    public async Task CreateAdminAccountAsync(AdminAccount adminAccount, CancellationToken cancellationToken = default)
    {
        await context.Admins.AddAsync(adminAccount, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
}