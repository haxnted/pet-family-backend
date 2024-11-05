using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Application.Commands.UpdateAccountSocialLinks;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.UpdateAccountRequisites;

public class UpdateAccountRequisitesHandler(
    IValidator<UpdateAccountRequisitesCommand> validator,
    UserManager<User> userManager,
    IVolunteerAccountManager volunteerAccountManager,
    CancellationToken cancellationToken = default) : ICommandHandler<UpdateAccountRequisitesCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        UpdateAccountRequisitesCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
            return Errors.User.InvalidCredentials().ToErrorList();

        var requisites = command.Requisites.Select(s => Requisite.Create(s.Name, s.Description).Value);

        var volunteerAccount = await volunteerAccountManager.GetVolunteerAccountByIdAsync(user.Id, cancellationToken);
        if (volunteerAccount is null)
            return Errors.General.NotFound(command.UserId).ToErrorList();
        
        volunteerAccount.Requisites = requisites.ToList();
        await volunteerAccountManager.UpdateAsync(volunteerAccount, cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
