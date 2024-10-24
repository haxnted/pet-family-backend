﻿using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerManagement.Domain.ValueObjects;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateRequisites;

public class UpdateRequisitesValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("Id"));
        
        RuleForEach(c => c.Requisites)
            .MustBeValueObject(s => Requisite.Create(s.Name, s.Description));
    }
}