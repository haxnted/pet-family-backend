namespace PetFamily.VolunteerManagement.Infrastructure.Options;

public class SoftDeleteInfo
{
    public const string NAME_SECTION = "SoftDeleteInfo";
    public int CountDaysBeforeHardDelete { get; init; }
}
