using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Application.Commands.UpdateFullName;

public class UpdateFullNameValidator : AbstractValidator<UpdateFullNameCommand>
{
    public UpdateFullNameValidator()
    {
        RuleFor(u => new { u.FullNameDto.Name, u.FullNameDto.Surname, u.FullNameDto.Patronymic })
            .MustBeValueObject(u => FullName.Create(u.Name, u.Surname, u.Patronymic));
    }
}
