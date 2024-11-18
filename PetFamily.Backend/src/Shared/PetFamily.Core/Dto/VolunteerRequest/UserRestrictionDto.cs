namespace PetFamily.Core.Dto.VolunteerRequest;

public class UserRestrictionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime BannedUntil { get; set; }
    public string Reason { get; set; }
}