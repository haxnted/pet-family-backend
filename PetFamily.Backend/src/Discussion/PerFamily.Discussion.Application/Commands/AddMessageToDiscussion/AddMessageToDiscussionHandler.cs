using CSharpFunctionalExtensions;
using FluentValidation;
using PerFamily.Discussion.Domain.Entities;
using PerFamily.Discussion.Domain.EntityIds;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PerFamily.Discussion.Application.Commands.AddMessageToDiscussion;

public class AddMessageToDiscussionHandler(
    IValidator<AddMessageToDiscussionCommand> validator,
    IDiscussionRepository repository)
    : ICommandHandler<AddMessageToDiscussionCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        AddMessageToDiscussionCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var discussion = await repository.GetById(command.DiscussionId, cancellationToken);
        if (discussion.IsFailure)
            return discussion.Error.ToErrorList();

        var messageId = MessageId.NewId();
        var text = Description.Create(command.Message).Value;
        var message = Message.Create(messageId, command.UserId, text);
        
        var result = discussion.Value.AddMessage(command.UserId, message);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        return UnitResult.Success<ErrorList>();

    }
    
}