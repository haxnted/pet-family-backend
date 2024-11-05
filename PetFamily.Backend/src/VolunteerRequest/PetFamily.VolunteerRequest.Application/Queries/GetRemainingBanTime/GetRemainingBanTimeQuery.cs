using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Queries.GetRemainingBanTime;

public record GetRemainingBanTimeQuery(Guid ParticipantId) : IQuery;
