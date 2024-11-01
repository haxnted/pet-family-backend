using System.Data;

namespace PerFamily.Discussion.Application;

public interface IDiscussionUnitOfWork
{
    public Task<IDbTransaction> BeginTransaction(CancellationToken token = default);
    public Task SaveChanges(CancellationToken token = default);
}
