namespace PetFamily.VolunteerRequest.Domain;

public enum TypeRequest
{
    Submitted,
    Rejected,
    RevisionRequired, // требует доработки
    Considered, // рассматривается
    Approved
}
