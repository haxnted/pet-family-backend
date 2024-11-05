using FluentValidation;
using PetFamily.Core.Dto.Discussions;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.ValidatorDtos;

public class MessageDtoValidator : AbstractValidator<MessageDto>
{
    public MessageDtoValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty().WithError(Errors.General.ValueIsRequired("MessageId"));

        RuleFor(m => m.Text)
            .MustBeValueObject(Description.Create);
        
        RuleFor(m => m.UserId)
            .NotEmpty().WithError(Errors.General.ValueIsRequired("UserId"));
        
        RuleFor(m => m.DiscussionId)
            .NotEmpty().WithError(Errors.General.ValueIsRequired("DiscussionId"));



    }   
}

