namespace PerFamily.Discussion.Domain.EntityIds;

public record MessageId
{
    public Guid Id { get; }
    
    private MessageId(Guid id) => Id = id;
    
    public static MessageId NewId() => new (Guid.NewGuid());
    public static MessageId Create(Guid id) => new(id);
    public static implicit operator Guid(MessageId discussionId) => discussionId.Id;
}