using CSharpFunctionalExtensions;
using PerFamily.Discussion.Application.Commands.AddMessageToDiscussion;
using PerFamily.Discussion.Application.Commands.CloseDiscussion;
using PerFamily.Discussion.Application.Commands.CreateDiscussion;
using PerFamily.Discussion.Contracts;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Presentation.Contracts;

public class DiscussionContract(
    CreateDiscussionHandler createDiscussionHandler,
    AddMessageToDiscussionHandler addMessageToDiscussionHandler,
    CloseDiscussionHandler closeDiscussionHandler) : IDiscussionContract
{
    public async Task<Result<Guid, ErrorList>> CreateDiscussion(Guid RelationId, Guid UserId, Guid AdminId)
    {
        var result = await createDiscussionHandler.Execute(new CreateDiscussionCommand(RelationId, UserId, AdminId));
        if (result.IsFailure)
            return result.Error;

        return result;
    }

    public async Task<UnitResult<ErrorList>> CloseDiscussionById(Guid DiscussionId, Guid AdminId)
    {
        var result = await closeDiscussionHandler.Execute(new CloseDiscussionCommand(DiscussionId, AdminId));
        if (result.IsFailure)
            return result.Error;

        return result;
    }

    public async Task<UnitResult<ErrorList>> AddMessage(Guid DiscussionId, Guid UserId, string Message) =>
        await addMessageToDiscussionHandler.Execute(new AddMessageToDiscussionCommand(DiscussionId, UserId, Message));
}
