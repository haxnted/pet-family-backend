using System.Data;

namespace PetFamily.VolunteerRequest.Application;

public interface IVolunteerRequestUnitOfWork
{
    public Task<IDbTransaction> BeginTransaction(CancellationToken token = default);
    public Task SaveChanges(CancellationToken token = default);
}
