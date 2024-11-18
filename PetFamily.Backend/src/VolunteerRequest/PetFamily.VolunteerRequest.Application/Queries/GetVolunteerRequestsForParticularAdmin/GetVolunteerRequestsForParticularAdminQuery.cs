using PetFamily.Core.Abstractions;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticularAdmin;

public record GetVolunteerRequestsForParticularAdminQuery(
    Guid AdminId,
    TypeRequest? SortByStatus,
    int Page,
    int PageSize) : IQuery;
