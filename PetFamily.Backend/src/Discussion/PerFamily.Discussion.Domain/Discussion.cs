using CSharpFunctionalExtensions;
using PerFamily.Discussion.Domain.Entities;
using PerFamily.Discussion.Domain.EntityIds;
using PerFamily.Discussion.Domain.Enums;
using PerFamily.Discussion.Domain.ValueObjects;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Domain;

public class Discussion : PetFamily.SharedKernel.Entity<DiscussionId>
{
    private List<Message> _messages = [];
    public Guid RelationId { get; private set; }
    public DiscussionStatus Status { get; private set; }
    public Users Users { get; set; }
    public IReadOnlyList<Message> Messages => _messages;

    private Discussion(DiscussionId id) : base(id) { }

    private Discussion(
        DiscussionId discussionId,
        Guid relationId,
        Users users,
        DiscussionStatus status) : this(discussionId)
    {
        RelationId = relationId;
        Users = users;
        Status = status;
    }

    public static Discussion Create(DiscussionId id, Guid relationId, Users users)
    {
        return new Discussion(id, relationId, users, DiscussionStatus.Open);
    }

    public UnitResult<Error> AddMessage(Guid userId, Message message)
    {
        if (Users.IsExisting(userId) == false)
            return Errors.General.NotFound(userId);

        _messages.Add(message);
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> DeleteMessage(Guid userId, MessageId messageId)
    {
        if (Users.IsExisting(userId) == false)
            return Errors.General.NotFound(userId);

        var message = Messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return Errors.General.NotFound(messageId);

        if (message.UserId != userId)
            return Errors.Discussion.NonCreator(userId);

        _messages.Remove(message);
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> EditingMessage(Guid userId, MessageId messageId, string text)
    {
        if (Users.IsExisting(userId) == false)
            return Errors.General.NotFound(userId);

        var message = Messages.FirstOrDefault(m => m.Id == messageId);
        if (message is null)
            return Errors.General.NotFound(messageId);

        if (message.UserId != userId)
            return Errors.Discussion.NonCreator(userId);

        var isMessageUpdated = message.UpdateText(text);
        if (isMessageUpdated.IsFailure)
            return isMessageUpdated.Error;

        return UnitResult.Success<Error>();
    }

    public void Close() => Status = DiscussionStatus.Closed;
}
