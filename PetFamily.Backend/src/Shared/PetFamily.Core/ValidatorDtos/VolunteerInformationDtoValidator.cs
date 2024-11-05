using FluentValidation;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.ValidatorDtos;

public class VolunteerInformationDtoValidator : AbstractValidator<VolunteerInformationDto>
{
    public VolunteerInformationDtoValidator()
    {
        RuleFor(c => c.FullName.Name)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("Name"));
        
        RuleFor(c => c.FullName.Surname)
            .NotEmpty().WithError(Errors.General.ValueIsInvalid("PhoneNumber"));

        RuleFor(c => c.AgeExperience)
            .MustBeValueObject(AgeExperience.Create);
        
        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(c => c.Description)
            .MustBeValueObject(Description.Create);
    }
}
