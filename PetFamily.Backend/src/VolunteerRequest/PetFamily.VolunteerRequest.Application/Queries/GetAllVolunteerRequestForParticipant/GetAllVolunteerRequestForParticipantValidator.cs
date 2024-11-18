using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetAllVolunteerRequestForParticipant;

public class
    GetAllVolunteerRequestForParticipantValidator : AbstractValidator<GetAllVolunteerRequestForParticipantQuery>
{
    public GetAllVolunteerRequestForParticipantValidator()
    {
        RuleFor(v => v.Page)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("Page"));

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}
