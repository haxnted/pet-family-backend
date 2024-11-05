using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;
using RejectionDescription = PetFamily.VolunteerRequest.Domain.ValueObjects.RejectionDescription;

namespace PetFamily.VolunteerRequest.Application.Commands.RejectApplication;

public class RejectApplicationHandler(
    IValidator<RejectApplicationCommand> validator,
    IUserRestrictionRepository userRestrictionRepository,
    IVolunteerRequestRepository volunteerRequestRepository,
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<RejectApplicationHandler> logger) : ICommandHandler<RejectApplicationCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        RejectApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteerRequest = await volunteerRequestRepository.GetById(command.VolunteerRequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var rejectDescription = RejectionDescription.Create(command.RejectDescription).Value;
        var result = volunteerRequest.Value.Reject(rejectDescription);
        if (result.IsFailure)
            return Error.Failure("failed.reject.application", "Failed to update application status").ToErrorList();

        volunteerRequestRepository.Save(volunteerRequest.Value);

        var userRestrictionId = UserRestrictionId.NewId();
        var userRestriction = UserRestriction.Create(userRestrictionId, volunteerRequest.Value.UserId, rejectDescription);
        if (userRestriction.IsFailure)
            return userRestriction.Error.ToErrorList();

        await userRestrictionRepository.Add(userRestriction.Value, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        logger.Log(LogLevel.Information,
            "{VolunteerRequestId} the application has been set to the status of corrections",
            command.VolunteerRequestId);

        return UnitResult.Success<ErrorList>();
    }
}