using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticularAdmin;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Presentation.Requests;

public record GetVolunteerRequestsForParticularAdminRequest(
    TypeRequest? SortByStatus,
    int Page,
    int PageSize)
{
    public GetVolunteerRequestsForParticularAdminQuery ToCommand(Guid adminId) => 
        new(adminId, SortByStatus, Page, PageSize);
}
