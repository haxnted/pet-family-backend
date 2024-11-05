using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PerFamily.Discussion.Contracts;
using PetFamily.Accounts.Contracts;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Commands.ApproveApplication;

public class ApproveApplicationHandler(
    IDiscussionContract discussionContract,
    IVolunteerRequestRepository repository,
    IAccountContract accountContract,
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<ApproveApplicationHandler> logger) : ICommandHandler<ApproveApplicationCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        ApproveApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await repository.GetById(command.VolunteerRequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        if (volunteerRequest.Value.InspectorId != command.AdminId)
            return Errors.General.AccessDenied("approve.application").ToErrorList();

        var resultApprove = volunteerRequest.Value.Approve();
        if (resultApprove.IsFailure)
            return Error.Failure("failed.update.status", "Failed to update application status").ToErrorList();

        var closeDiscussionResult
            = await discussionContract.CloseDiscussionById(volunteerRequest.Value.DiscussionId, command.AdminId);
        if (closeDiscussionResult.IsFailure)
            return closeDiscussionResult.Error;

        var resultCreateAccount = await CreateVolunteerAccount(volunteerRequest.Value);
        if (resultCreateAccount.IsFailure)
            return resultCreateAccount.Error;

        await unitOfWork.SaveChanges(cancellationToken);
        
        logger.Log(LogLevel.Information, "{VolunteerRequestId} changed status to approved", command.VolunteerRequestId);

        return UnitResult.Success<ErrorList>();
    }

    private async Task<UnitResult<ErrorList>> CreateVolunteerAccount(Domain.VolunteerRequest volunteerRequest)
    {
        return await accountContract.CreateVolunteerAccount(volunteerRequest.UserId,
            volunteerRequest.Information.AgeExperience,
            volunteerRequest.Information.Requisites);
    }
}
