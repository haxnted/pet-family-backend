using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;

public class GetAllVolunteerRequestForParticipantHandler(
    IValidator<GetAllVolunteerRequestForParticipantQuery> validator,
    IVolunteerRequestReadDbContext volunteersReadDbContext)
    : IQueryHandler<PagedList<VolunteerRequestDto>, GetAllVolunteerRequestForParticipantQuery>
{
    public async Task<Result<PagedList<VolunteerRequestDto>, ErrorList>> Execute(
        GetAllVolunteerRequestForParticipantQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteersQuery = volunteersReadDbContext.VolunteerRequests;

        volunteersQuery.WhereIf(query.ParticipantId != Guid.Empty, request => request.UserId == query.ParticipantId);
        volunteersQuery.WhereIf(query.SortByStatus.HasValue, request => request.Status == query.SortByStatus);

        var result = await volunteersQuery.GetObjectsWithPagination(query.Page, query.PageSize, cancellationToken);

        return result;
    }
}
