using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerRequest.Application.Commands.TakeApplicationForReview;

public record TakeApplicationForReviewCommand(Guid AdminId, Guid VolunteerRequestId) : ICommand;
