using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Application;

public interface IUserRestrictionRepository
{
    public Task Add(UserRestriction userRestriction, CancellationToken cancellationToken = default);

    public void Save(UserRestriction userRestriction);

    public Task<Result<UserRestriction, Error>> GetByUserId(Guid userId, CancellationToken cancellationToken = default);
}
