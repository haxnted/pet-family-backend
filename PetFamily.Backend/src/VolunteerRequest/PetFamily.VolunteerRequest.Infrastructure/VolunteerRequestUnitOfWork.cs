using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.VolunteerRequest.Application;

namespace PetFamily.VolunteerRequest.Infrastructure;

public class VolunteerRequestUnitOfWork(VolunteerRequestWriteDbContext context) : IVolunteerRequestUnitOfWork
{
    public async Task<IDbTransaction> BeginTransaction(CancellationToken token = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(token);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken token = default) =>
        await context.SaveChangesAsync(token);
}
