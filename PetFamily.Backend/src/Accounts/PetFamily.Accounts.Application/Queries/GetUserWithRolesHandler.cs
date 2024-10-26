using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.Accounts;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application.Queries;

public class GetUserWithRolesHandler(
    IValidator<GetUserWithRolesQuery> validator,
    RoleManager<Role> userManager,
    IAccountsReadDbContext accountsDbContext)
    : IQueryHandler<UserDto, GetUserWithRolesQuery>
{
    public async Task<Result<UserDto, ErrorList>> Execute(
        GetUserWithRolesQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();
        
        var user = await accountsDbContext.Users.FirstOrDefaultAsync(user => user.Id == query.UserId,
            cancellationToken);
        if (user is null)
            return Errors.General.NotFound(query.UserId).ToErrorList();

        var role = await userManager.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId, cancellationToken);
        if (role is null)
            return Errors.General.NotFound(query.UserId).ToErrorList();

        user.AdminAccountDto = await accountsDbContext.Admins.FirstOrDefaultAsync(r => r.UserId == query.UserId,
            cancellationToken);

        user.ParticipantAccountDto
            = await accountsDbContext.Participants.FirstOrDefaultAsync(r => r.UserId == query.UserId,
                cancellationToken);

        user.VolunteerAccountDto
            = await accountsDbContext.Volunteers.FirstOrDefaultAsync(r => r.UserId == query.UserId, cancellationToken);

        return user;

    }
}