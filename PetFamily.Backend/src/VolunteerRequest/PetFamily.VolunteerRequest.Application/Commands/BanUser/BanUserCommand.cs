using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.BanUser;

public record BanUserCommand(Guid ParticipantId, int BanDurationDays, string Reason) : ICommand;
