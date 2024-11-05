using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsWithPagination;

public class GetVolunteerRequestsWithPaginationHandler(IVolunteerRequestReadDbContext context) :
    IQueryHandler<PagedList<VolunteerRequestDto>, GetVolunteerRequestsWithPaginationQuery>
{
    private const int SUBMITTED_STATUS = 0;

    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Execute(
        GetVolunteerRequestsWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        var volunteersQuery = context.VolunteerRequests.Where(v => v.Status == SUBMITTED_STATUS);

        var keySelector = SortByProperty(query.SortBy);

        volunteersQuery = query.SortDirection?.ToLower() == "desc"
            ? volunteersQuery.OrderByDescending(keySelector)
            : volunteersQuery.OrderBy(keySelector);

        return await volunteersQuery.GetObjectsWithPagination(query.Page, query.PageSize, cancellationToken);
    }

    private static Expression<Func<VolunteerRequestDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return volunteer => volunteer.Id;

        Expression<Func<VolunteerRequestDto, object>> keySelector = sortBy?.ToLower() switch
        {
            "age" => volunteer => volunteer.Information.AgeExperience,
            "createdAt" => volunteer => volunteer.CreatedAt,
            _ => volunteer => volunteer.Id
        };
        return keySelector;
    }
}
