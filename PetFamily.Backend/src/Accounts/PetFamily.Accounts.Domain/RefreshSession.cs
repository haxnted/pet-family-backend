namespace PetFamily.Accounts.Domain;

public class RefreshSession
{
    public RefreshSession(){ }

    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; } = default!;
    public Guid RefreshToken { get; init; }
    public Guid Jti { get; set; }
    public DateTime ExpiredAt { get; init; }
    public DateTime CreatedAt { get; init; }
}
