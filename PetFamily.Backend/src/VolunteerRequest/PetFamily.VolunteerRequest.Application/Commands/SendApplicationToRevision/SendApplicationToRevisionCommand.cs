using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.SendApplicationToRevision;

public record SendApplicationToRevisionCommand(Guid AdminId, Guid VolunteerRequestId, string Message) : ICommand;
