using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application;

public interface IRefreshSessionManager
{
    public Task<Result<RefreshSession, Error>> GetByRefreshTokenAsync(
        Guid refreshToken, CancellationToken cancellationToken = default);

    public void Delete(RefreshSession refreshSession);
}
