using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application;

public interface IVolunteerRequestRepository
{
    public Task Add(Domain.VolunteerRequest volunteerRequest, CancellationToken cancellationToken = default);

    public void Save(Domain.VolunteerRequest volunteer);
    
    public Task<Result<Domain.VolunteerRequest, Error>> GetById(
        Guid id, CancellationToken cancellationToken = default);

    public Task<Result<Domain.VolunteerRequest, Error>> GetByUserId(
        Guid userId, CancellationToken cancellationToken = default);
}
