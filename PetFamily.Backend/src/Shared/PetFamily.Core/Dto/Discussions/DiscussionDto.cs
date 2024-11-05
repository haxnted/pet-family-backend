namespace PetFamily.Core.Dto.Discussions;

public class DiscussionDto
{
    public Guid Id { get; set; }
    public Guid RelationId { get; set; }
    public int Status { get; set; }
    public Guid ParticipantId { get; set; }
    public Guid AdminId { get; set; }
    public IReadOnlyList<MessageDto> Messages { get; set; }
}
