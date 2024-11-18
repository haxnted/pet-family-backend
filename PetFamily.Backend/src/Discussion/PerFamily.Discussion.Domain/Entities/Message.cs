using CSharpFunctionalExtensions;
using PerFamily.Discussion.Domain.EntityIds;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PerFamily.Discussion.Domain.Entities;

public class Message : PetFamily.SharedKernel.Entity<MessageId>
{
    public Description Text { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsEdited { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DiscussionId DiscussionId { get; private set; }

    private Message(MessageId id) : base(id)
    {
    }

    public Message(MessageId id, Guid userId, Description text, bool isEdited, DateTime createdAt) : base(id)
    {
        UserId = userId;
        Text = text;
        IsEdited = isEdited;
        CreatedAt = createdAt;
    }

    public static Message Create(MessageId id, Guid userId, Description text) =>
        new(id, userId, text, false, DateTime.UtcNow);

    public UnitResult<Error> UpdateText(string text)
    {
        var newText = Description.Create(text);
        if (newText.IsFailure)
            return newText.Error;

        Text = newText.Value;
        IsEdited = true;

        return UnitResult.Success<Error>();
    }
}
