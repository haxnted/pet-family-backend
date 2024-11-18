using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.UpdateAccountSocialLinks;

public class UpdateAccountSocialLinksHandler(
    IValidator<UpdateAccountSocialLinksCommand> validator,
    UserManager<User> userManager) : ICommandHandler<UpdateAccountSocialLinksCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        UpdateAccountSocialLinksCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
            return Errors.User.InvalidCredentials().ToErrorList();

        var socialLinks = command.SocialLinks.Select(s => SocialLink.Create(s.Url, s.Name).Value);
        user.SocialLinks = socialLinks.ToList();
        
        await userManager.UpdateAsync(user);
        
        return UnitResult.Success<ErrorList>();
    }
}
