using PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Presentation.Requests;

public record GetVolunteerRequestsForParticipantRequest(TypeRequest SortByStatus, int Page, int PageSize)
{
    public GetAllVolunteerRequestForParticipantQuery ToQuery(Guid participantId) => new(participantId, SortByStatus, Page, PageSize);
}
