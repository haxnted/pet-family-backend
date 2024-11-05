using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Queries.GetRestrictionUsersWithPagination;

public record GetRestrictionUsersWithPaginationQuery(int Page, int PageSize) : IQuery;
