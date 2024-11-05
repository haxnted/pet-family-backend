using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Core.Extensions;
using PetFamily.Framework;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Presentation;

public class AccountContract(
    PermissionManager permissionManager,
    IVolunteerAccountManager volunteerAccountManager,
    UserManager<User> userManager) : IAccountContract
{
    public async Task<Result<IEnumerable<string>,Error>> GetPermissionsUserById(
        Guid userId, CancellationToken cancellationToken = default)
    {
        return await permissionManager.GetPermissionsByUserId(userId, cancellationToken);
    }

    public async Task<UnitResult<Error>> IsUserExists(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == userId,
            cancellationToken: cancellationToken);
        if (user is null)
            return Errors.General.NotFound(userId);

        return UnitResult.Success<Error>();
    }

    public async Task<UnitResult<ErrorList>> CreateVolunteerAccount(
        Guid userId,
        AgeExperience experience,
        IEnumerable<Requisite> requisites,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user is null)
            return Errors.General.NotFound(userId).ToErrorList();

        var isUserAlreadyVolunteer = user.Roles.Any(r=>r.Name == Roles.Volunteer);
        if (isUserAlreadyVolunteer)
            return Errors.General.AlreadyExist("VolunteerAccount").ToErrorList();
            
        var resultAddRole = await userManager.AddToRoleAsync(user, Roles.Volunteer);
        if (resultAddRole.Succeeded == false)
            return resultAddRole.Errors.ToList();
        
        var volunteerAccount = new VolunteerAccount(experience.Years, requisites.ToList(), user);

        await volunteerAccountManager.CreateVolunteerAccountAsync(volunteerAccount, cancellationToken);

        return UnitResult.Success<ErrorList>();
    }

    
}
