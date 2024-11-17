using PetFamily.Core.Abstractions;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;

public record GetAllVolunteerRequestForParticipantQuery(
    Guid ParticipantId,
    TypeRequest? SortByStatus,
    int Page,
    int PageSize) : IQuery;
