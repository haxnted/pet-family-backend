namespace PetFamily.Core.Dto.VolunteerRequest;

public class VolunteerInformationDto
{
    public FullNameDto FullName { get; init; } = default!;
    public int AgeExperience { get; init; } = default!;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<RequisiteDto> Requisites { get; init; } = [];
}
