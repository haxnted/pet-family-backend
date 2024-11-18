using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.RejectApplication;

public record RejectApplicationCommand(Guid AdminId, Guid VolunteerRequestId, string RejectDescription) : ICommand;
