using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticularAdmin;

public class GetVolunteerRequestsForParticularAdminHandler(
    IValidator<GetVolunteerRequestsForParticularAdminQuery> validator,
    IVolunteerRequestReadDbContext volunteersReadDbContext)
    : IQueryHandler<PagedList<VolunteerRequestDto>, GetVolunteerRequestsForParticularAdminQuery>
{
    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Execute(
        GetVolunteerRequestsForParticularAdminQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteersQuery = volunteersReadDbContext.VolunteerRequests;

        volunteersQuery.WhereIf(query.AdminId != Guid.Empty, request => request.InspectorId == query.AdminId);
        volunteersQuery.WhereIf(query.SortByStatus.HasValue, request => request.Status == query.SortByStatus);

        var result = await volunteersQuery.GetObjectsWithPagination(query.Page, query.PageSize, cancellationToken);

        return result;
    }
}
