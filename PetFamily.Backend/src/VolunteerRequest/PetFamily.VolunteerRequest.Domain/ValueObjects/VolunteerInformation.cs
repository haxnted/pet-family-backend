using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.VolunteerRequest.Domain.ValueObjects;

public record VolunteerInformation
{
    private VolunteerInformation() { }

    public VolunteerInformation(
        FullName fullName, 
        AgeExperience ageExperience, 
        PhoneNumber phoneNumber,
        Description description,
        List<Requisite> requisites)
    {
        FullName = fullName;
        AgeExperience = ageExperience;
        PhoneNumber = phoneNumber;
        Description = description;
        Requisites = requisites;
    }

    public FullName FullName { get; }
    public AgeExperience AgeExperience { get;}
    public PhoneNumber PhoneNumber { get; }
    public Description Description { get; }
    public List<Requisite> Requisites { get; }
}
