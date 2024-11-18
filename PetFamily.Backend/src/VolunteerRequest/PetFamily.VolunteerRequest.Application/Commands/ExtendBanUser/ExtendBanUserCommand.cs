using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.ExtendBanUser;

public record ExtendBanUserCommand(Guid ParticipantId, int AdditionalDays) : ICommand;
