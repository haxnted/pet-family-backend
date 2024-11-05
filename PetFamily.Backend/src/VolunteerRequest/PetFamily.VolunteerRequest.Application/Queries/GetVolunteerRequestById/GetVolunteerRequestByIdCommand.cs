using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestById;

public record GetVolunteerRequestByIdCommand(Guid VolunteerRequestId) :  IQuery;
