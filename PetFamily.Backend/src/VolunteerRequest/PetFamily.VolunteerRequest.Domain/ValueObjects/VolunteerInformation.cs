using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.VolunteerRequest.Domain.ValueObjects;

public record VolunteerInformation(
    FullName FullName,
    AgeExperience AgeExperience,
    PhoneNumber PhoneNumber,
    Description Description,
    List<SocialLink> SocialLinks);
