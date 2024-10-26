namespace PetFamily.Core.Dto.Accounts;

public class VolunteerAccountDto
{
    public Guid Id { get; init; }
    public int Experience { get; init; }
    public List<RequisiteDto> Requisites { get; init; } = [];
    public Guid UserId { get; init; }
}