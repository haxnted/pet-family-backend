using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticularAdmin;

public class
    GetVolunteerRequestsForParticularAdminValidator : AbstractValidator<GetVolunteerRequestsForParticularAdminQuery>
{
    public GetVolunteerRequestsForParticularAdminValidator()
    {
        RuleFor(v => v.Page)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("Page"));

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("PageSize"));
    }
}
