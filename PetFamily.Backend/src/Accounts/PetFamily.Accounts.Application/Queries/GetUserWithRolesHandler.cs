using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.Accounts;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application.Queries;

public class GetUserWithRolesHandler(
    IValidator<GetUserWithRolesQuery> validator,
    IAccountsReadDbContext accountsDbContext)
    : IQueryHandler<UserDto, GetUserWithRolesQuery>
{
    public async Task<Result<UserDto, ErrorList>> Execute(
        GetUserWithRolesQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var user = await GetUserById(query.UserId, cancellationToken);
        if (user is null)
            return Errors.General.NotFound(query.UserId).ToErrorList();
        
        return user;
    }
    
    private async Task<UserDto?> GetUserById(Guid userId, CancellationToken cancellationToken) =>
        await accountsDbContext.Users.Include(u => u.ParticipantAccount)
            .Include(u => u.VolunteerAccount)
            .Include(u => u.AdminAccount)
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
}