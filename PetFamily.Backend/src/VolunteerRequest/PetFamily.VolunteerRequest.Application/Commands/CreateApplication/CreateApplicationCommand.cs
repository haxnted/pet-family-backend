using PetFamily.Core.Abstractions;
using PetFamily.Core.Dto.VolunteerRequest;

namespace PetFamily.VolunteerRequest.Application.Commands.CreateApplication;

public record CreateApplicationCommand(
    Guid ParticipantId,
    VolunteerInformationDto VolunteerInformation) : ICommand;
