using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetRestrictionUsersWithPagination;

public class GetRestrictionUsersWithPaginationHandler(
    IValidator<GetRestrictionUsersWithPaginationQuery> validator,
    IVolunteerRequestReadDbContext context)
    : IQueryHandler<PagedList<UserRestrictionDto>, GetRestrictionUsersWithPaginationQuery>
{
    public async Task<Result<PagedList<UserRestrictionDto>, ErrorList>> Execute(
        GetRestrictionUsersWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var userRestrictions =  context.UserRestrictions;

        return await userRestrictions.GetObjectsWithPagination(query.Page, query.PageSize, cancellationToken);
    }
    private static Expression<Func<UserRestrictionDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return volunteer => volunteer.Id;

        Expression<Func<UserRestrictionDto, object>> keySelector = sortBy?.ToLower() switch
        {
            "BannedUntil" => userRestriction => userRestriction.BannedUntil,
            _ => userRestriction => userRestriction.Id
        };
        return keySelector;
    }
}