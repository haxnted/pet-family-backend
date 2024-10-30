namespace PerFamily.Discussion.Domain.ValueObjects;

public record Users
{
    public Guid UserId { get; private set; }
    public Guid AdminId { get; private set; }

    public Users(Guid userId, Guid adminId)
    {
        UserId = userId;
        AdminId = adminId;
    }

    public bool IsExisting(Guid userId) =>
        userId == UserId || userId == AdminId;
    
}
