using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.UpdateAccountRequisites;

public class UpdateAccountRequisitesCommandValidator : AbstractValidator<UpdateAccountRequisitesCommand>
{
    public UpdateAccountRequisitesCommandValidator()
    {
        RuleFor(l => l.UserId).NotEmpty();
        
        RuleForEach(l => l.Requisites)
            .MustBeValueObject(l => Requisite.Create(l.Name, l.Description));
    }
}
