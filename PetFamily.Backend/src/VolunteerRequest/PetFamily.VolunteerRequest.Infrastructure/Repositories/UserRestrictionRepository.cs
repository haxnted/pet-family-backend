using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Infrastructure.Repositories;

public class UserRestrictionRepository(VolunteerRequestWriteDbContext context) : IUserRestrictionRepository
{
    public async Task Add(UserRestriction userRestriction, CancellationToken cancellationToken = default)
    {
        await context.UserRestrictions.AddAsync(userRestriction, cancellationToken);
    }

    public void Save(UserRestriction userRestriction)
    {
        context.UserRestrictions.Attach(userRestriction);
    }

    public async Task<Result<UserRestriction, Error>> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await context.UserRestrictions
            .FirstOrDefaultAsync(v => v.UserId == userId, cancellationToken);
        
        if (volunteerRequest is null)
            return Errors.General.NotFound(userId);

        return volunteerRequest;        
    }

    public async Task Delete(UserRestriction userRestriction, CancellationToken cancellationToken = default)
    {
        context.Remove(userRestriction);
    }
}
