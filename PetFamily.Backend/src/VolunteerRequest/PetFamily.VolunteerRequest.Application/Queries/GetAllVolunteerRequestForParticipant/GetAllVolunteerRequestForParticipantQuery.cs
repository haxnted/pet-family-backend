using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;

public record GetAllVolunteerRequestForParticipantQuery(
    Guid ParticipantId,
    int? SortByStatus,
    int Page,
    int PageSize) : IQuery;
