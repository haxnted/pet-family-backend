using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Application.Commands.CreateDiscussion;

public class CreateDiscussionCommandValidator : AbstractValidator<CreateDiscussionCommand>
{
    public CreateDiscussionCommandValidator()
    {
        RuleFor(x => x.RelationId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("RelationId"));
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("UserId"));
        
        RuleFor(x => x.AdminId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("AdminId"));
    }
}
