using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Core.ValidatorDtos;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Commands.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationCommandValidator()
    {
        RuleFor(c => c.ParticipantId)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("AdminId"));

        RuleFor(c => c.VolunteerInformation)
            .NotNull().WithError(Errors.General.ValueIsInvalid("VolunteerInformation"));

        RuleFor(c => c.VolunteerInformation)
            .SetValidator(new VolunteerInformationDtoValidator());
    }
}
