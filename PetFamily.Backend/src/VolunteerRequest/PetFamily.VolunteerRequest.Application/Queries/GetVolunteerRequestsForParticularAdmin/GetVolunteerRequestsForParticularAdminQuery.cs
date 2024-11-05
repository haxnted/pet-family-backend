using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticularAdmin;

public record GetVolunteerRequestsForParticularAdminQuery(
    Guid AdminId,
    int? SortByStatus,
    int Page,
    int PageSize) : IQuery;
