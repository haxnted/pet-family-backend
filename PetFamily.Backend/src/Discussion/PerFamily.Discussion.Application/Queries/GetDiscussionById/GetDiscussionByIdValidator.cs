using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Application.Queries.GetDiscussionById;

public class GetDiscussionByIdValidator : AbstractValidator<GetDiscussionByIdQuery>
{
    public GetDiscussionByIdValidator()
    {
        RuleFor(g => g.DiscussionId)
            .NotEmpty().WithError(Errors.General.ValueIsRequired("DiscussionId"));
    }
}
