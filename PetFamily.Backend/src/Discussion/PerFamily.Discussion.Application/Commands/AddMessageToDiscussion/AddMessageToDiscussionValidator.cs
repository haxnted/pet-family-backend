using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PerFamily.Discussion.Application.Commands.AddMessageToDiscussion;

public class AddMessageToDiscussionValidator : AbstractValidator<AddMessageToDiscussionCommand>
{
    public AddMessageToDiscussionValidator()
    {
        RuleFor(a => a.DiscussionId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("DiscussionId"));
        
        RuleFor(a => a.UserId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("UserId"));

        RuleFor(a => a.Message)
            .MustBeValueObject(Description.Create);
    }
}
