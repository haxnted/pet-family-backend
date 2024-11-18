using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.VolunteerRequest.Application.Commands.CreateApplication;

namespace PetFamily.VolunteerRequest.Presentation.Requests;

public record CreateApplicationRequest(VolunteerInformationDto VolunteerInformation)
{
    public CreateApplicationCommand ToCommand(Guid patricipantId) =>
        new(patricipantId, VolunteerInformation);
}