using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Contracts;

public interface IAccountContract
{
    public Task<Result<IEnumerable<string>, Error>> GetPermissionsUserById(
        Guid userId, CancellationToken cancellationToken = default);

    public Task<UnitResult<Error>> IsUserExists(Guid userId, CancellationToken cancellationToken = default);

    public Task<UnitResult<ErrorList>> CreateVolunteerAccount(
        Guid userId,
        AgeExperience experience,
        IEnumerable<Requisite> requisites,
        CancellationToken cancellationToken = default);
}
