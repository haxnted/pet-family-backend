using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PerFamily.Discussion.Contracts;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Domain.EntityIds;

namespace PetFamily.VolunteerRequest.Application.Commands.TakeApplicationForReview;

public class TakeApplicationForReviewHandler(
    IVolunteerRequestRepository repository,
    IVolunteerRequestUnitOfWork unitOfWork,
    IDiscussionContract discussionContract,
    ILogger<TakeApplicationForReviewHandler> logger) : ICommandHandler<TakeApplicationForReviewCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        TakeApplicationForReviewCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await repository.GetById(command.VolunteerRequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return Errors.General.NotFound(command.VolunteerRequestId).ToErrorList();
        
        var volunteerRequestId = volunteerRequest.Value.Id.Id;
        var participantId = volunteerRequest.Value.UserId;
        var adminId = command.AdminId;
        
        var transaction = await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            volunteerRequest.Value.Considered(adminId);
            var discussionId = await discussionContract.CreateDiscussion(volunteerRequestId, participantId, adminId);
            if (discussionId.IsFailure)
                return discussionId.Error;

            var result = volunteerRequest.Value.CreateDiscussion(discussionId.Value);
            if (result.IsFailure)
                return result.Error.ToErrorList();

            repository.Save(volunteerRequest.Value);
            await unitOfWork.SaveChanges(cancellationToken);
            transaction.Commit();

            logger.Log(LogLevel.Information, 
                "Volunteer request {VolunteerRequestId} taken for review by {AdminId}",
                volunteerRequestId, adminId);
        }
        catch (Exception e)
        {
            transaction.Rollback();
            logger.LogError(e, "Failed to take volunteer request {VolunteerRequestId} for review by {AdminId}",
                volunteerRequestId, command.AdminId);

            return Error.Failure("failed.to.take.volunteer.request.for.review",
                "Failed to take volunteer request for review", null).ToErrorList();
        }

        return UnitResult.Success<ErrorList>();
    }
}
