using FluentValidation;
using PetFamily.Core.ValidatorDtos;

namespace PetFamily.VolunteerRequest.Application.Commands.UpdateApplication;

public class UpdateApplicationValidator : AbstractValidator<UpdateApplicationCommand>
{
    public UpdateApplicationValidator()
    {
        RuleFor(u => u.VolunteerInformation)
            .SetValidator(new VolunteerInformationDtoValidator());
    }
}
