namespace PerFamily.Discussion.Domain.EntityIds;

public record DiscussionId
{
    public Guid Id { get; }
    
    private DiscussionId(Guid id) => Id = id;
    
    public static DiscussionId NewId() => new (Guid.NewGuid());
    public static DiscussionId Create(Guid id) => new(id);
    public static implicit operator Guid(DiscussionId discussionId) => discussionId.Id;
}

