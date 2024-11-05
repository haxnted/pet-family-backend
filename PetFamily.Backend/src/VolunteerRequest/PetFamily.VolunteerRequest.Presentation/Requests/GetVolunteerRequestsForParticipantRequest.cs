using PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;

namespace PetFamily.VolunteerRequest.Presentation.Requests;

public record GetVolunteerRequestsForParticipantRequest(int SortByStatus, int Page, int PageSize)
{
    public GetAllVolunteerRequestForParticipantQuery ToQuery(Guid participantId) => new(participantId, SortByStatus, Page, PageSize);
}
