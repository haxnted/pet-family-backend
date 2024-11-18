using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Commands.ExtendBanUser;

public class ExtendBanUserCommandValidator : AbstractValidator<ExtendBanUserCommand>
{
    public ExtendBanUserCommandValidator()
    {
        RuleFor(t => t.ParticipantId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("ParticipantId"));

        RuleFor(t => t.AdditionalDays)
            .GreaterThan(0).WithError(Errors.UserRestriction.InvalidBanDuration());
    }
}
