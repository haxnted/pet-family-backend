using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application.Queries;

public class GetUserWithRolesValidator : AbstractValidator<GetUserWithRolesQuery>
{
    public GetUserWithRolesValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty().WithError(Errors.General.ValueIsRequired("userId"));
    }
}
