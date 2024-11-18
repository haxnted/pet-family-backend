using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.Core.Dto.VolunteerRequest;

public class VolunteerRequestDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid InspectorId { get; init; }
    public Guid DiscussionId { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? RejectionDescription { get; init; } = string.Empty;
    public VolunteerInformationDto Information { get; init; } = default!;
    public TypeRequest Status { get; init; }
}
