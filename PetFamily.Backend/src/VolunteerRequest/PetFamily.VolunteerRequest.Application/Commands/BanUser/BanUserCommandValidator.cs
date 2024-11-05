using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Application.Commands.BanUser;

public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
{
    public BanUserCommandValidator()
    {
        RuleFor(t => t.BanDurationDays)
            .GreaterThan(0).WithError(Errors.UserRestriction.InvalidBanDuration());

        RuleFor(t => t.Reason)
            .MustBeValueObject(RejectionDescription.Create);
    }
}
