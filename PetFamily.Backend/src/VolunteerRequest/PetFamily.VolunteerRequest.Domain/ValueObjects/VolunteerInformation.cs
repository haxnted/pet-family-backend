using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.VolunteerRequest.Domain.ValueObjects;

public class VolunteerInformation
{
    private VolunteerInformation() { }

    public VolunteerInformation(
        FullName fullName, 
        AgeExperience ageExperience, 
        PhoneNumber phoneNumber,
        Description description,
        List<SocialLink> socialLinks)
    {
        FullName = fullName;
        AgeExperience = ageExperience;
        PhoneNumber = phoneNumber;
        Description = description;
        SocialLinks = socialLinks;
    }

    public FullName FullName { get; private set; }
    public AgeExperience AgeExperience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Description Description { get; private set; }
    public List<SocialLink> SocialLinks { get; private set; }
}
