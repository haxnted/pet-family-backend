using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.ApproveApplication;

public record ApproveApplicationCommand(Guid AdminId, Guid VolunteerRequestId) : ICommand;
