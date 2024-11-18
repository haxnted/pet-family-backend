using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.UnbanUser;

public record UnbanUserCommand(Guid ParticipantId) : ICommand;
