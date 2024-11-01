using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PerFamily.Discussion.Application;

namespace PerFamily.Discussion.Infrastructure;

public class DiscussionUnitOfWork(DiscussionWriteDbContext context) : IDiscussionUnitOfWork
{
    public async Task<IDbTransaction> BeginTransaction(CancellationToken token = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(token);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken token = default) =>
        await context.SaveChangesAsync(token);
}
