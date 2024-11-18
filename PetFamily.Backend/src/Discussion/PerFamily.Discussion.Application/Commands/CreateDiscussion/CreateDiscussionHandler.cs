using CSharpFunctionalExtensions;
using FluentValidation;
using PerFamily.Discussion.Domain.EntityIds;
using PerFamily.Discussion.Domain.ValueObjects;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Application.Commands.CreateDiscussion;

public class CreateDiscussionHandler(
    IValidator<CreateDiscussionCommand> validator,
    IDiscussionUnitOfWork unitOfWork,
    IDiscussionRepository repository)
    : ICommandHandler<Guid, CreateDiscussionCommand>
{
    public async Task<Result<Guid,ErrorList>> Execute(CreateDiscussionCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToList();
        
        var isDiscussionExists = await repository.GetByRelationId(command.RelationId, cancellationToken);
        if (isDiscussionExists.IsSuccess)
            return Errors.General.AlreadyExist("discussion").ToErrorList();
        
        var discussionId = DiscussionId.NewId();
        var users = new Users(command.UserId, command.AdminId);
        var discussion = Domain.Discussion.Create(discussionId, command.UserId, users);

        await repository.Add(discussion, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);
        
        return discussionId.Id;
    }
}