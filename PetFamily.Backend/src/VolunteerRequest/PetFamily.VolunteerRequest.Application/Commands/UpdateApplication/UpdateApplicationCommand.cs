using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;

namespace PetFamily.VolunteerRequest.Application.Commands.UpdateApplication;

public record UpdateApplicationCommand(Guid ParticipantId, Guid VolunteerRequestId, VolunteerInformationDto VolunteerInformation) : ICommand;
