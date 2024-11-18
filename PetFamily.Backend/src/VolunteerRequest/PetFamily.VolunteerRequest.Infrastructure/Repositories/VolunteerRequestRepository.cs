using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Application;

namespace PetFamily.VolunteerRequest.Infrastructure.Repositories;

public class VolunteerRequestRepository(VolunteerRequestWriteDbContext context) : IVolunteerRequestRepository
{
    public async Task Add(Domain.VolunteerRequest volunteerRequest, CancellationToken cancellationToken = default)
    {
       await context.VolunteerRequests.AddAsync(volunteerRequest, cancellationToken);
    }

    public void Save(Domain.VolunteerRequest volunteer)
    {
        context.VolunteerRequests.Attach(volunteer);
    }

    public async Task<Result<Domain.VolunteerRequest, Error>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await context.VolunteerRequests
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        
        if (volunteerRequest is null)
            return Errors.General.NotFound(id);

        return volunteerRequest;        
    }

    public async Task<Result<Domain.VolunteerRequest, Error>> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await context.VolunteerRequests
            .FirstOrDefaultAsync(v => v.UserId == userId, cancellationToken);
        
        if (volunteerRequest is null)
            return Errors.General.NotFound(userId);

        return volunteerRequest;
    }
}
