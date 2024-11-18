using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Application.Commands.RejectApplication;

public class SendApplicationToRevisionValidator : AbstractValidator<RejectApplicationCommand>
{
    public SendApplicationToRevisionValidator()
    {
        RuleFor(t => t.AdminId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("AdminId"));

        RuleFor(t => t.VolunteerRequestId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerRequestId"));

        RuleFor(s => s.RejectDescription)
            .MustBeValueObject(RejectionDescription.Create);
    }
}
