using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.UpdateAccountSocialLinks;

public class UpdateAccountSocialLinksCommandValidator : AbstractValidator<UpdateAccountSocialLinksCommand>
{
    public UpdateAccountSocialLinksCommandValidator()
    {
        RuleFor(l => l.UserId).NotEmpty();
        
        RuleForEach(l => l.SocialLinks)
            .MustBeValueObject(l => SocialLink.Create(l.Name, l.Url));
    }
}
