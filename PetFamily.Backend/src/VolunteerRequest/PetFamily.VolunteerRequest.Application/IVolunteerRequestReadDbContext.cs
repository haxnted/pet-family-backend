using PetFamily.Core.Dto.VolunteerRequest;

namespace PetFamily.VolunteerRequest.Application;

public interface IVolunteerRequestReadDbContext
{
    public IQueryable<VolunteerRequestDto> VolunteerRequests { get; }
    public IQueryable<UserRestrictionDto> UserRestrictions { get; } 
}
