namespace PetFamily.VolunteerRequest.Domain.EntityIds;

public record UserRestrictionId
{
    public Guid Id { get; }
    
    private UserRestrictionId(Guid id) => Id = id;
    
    public static UserRestrictionId NewId() => new (Guid.NewGuid());
    public static UserRestrictionId Create(Guid id) => new(id);
    public static implicit operator Guid(UserRestrictionId volunteerRequestRestrictionId) => volunteerRequestRestrictionId.Id;
}

