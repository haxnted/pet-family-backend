using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PerFamily.Discussion.Contracts;
using PerFamily.Discussion.Domain.Entities;
using PerFamily.Discussion.Domain.EntityIds;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Application.Commands.SendApplicationToRevision;

public class SendApplicationToRevisionHandler(
    IValidator<SendApplicationToRevisionCommand> validator,
    IVolunteerRequestRepository repository,
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<SendApplicationToRevisionHandler> logger) : ICommandHandler<SendApplicationToRevisionCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        SendApplicationToRevisionCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteerRequest = await repository.GetById(command.VolunteerRequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();
        
        var rejectDescription = RejectionDescription.Create(command.Message).Value;
        
        var result = volunteerRequest.Value.SendToRevision(rejectDescription);
        if (result.IsFailure)
            return Error.Failure("failed.to.add.message", "Failed to add message to revision").ToErrorList();

        repository.Save(volunteerRequest.Value);
        await unitOfWork.SaveChanges(cancellationToken);

        logger.Log(LogLevel.Information,
            "{VolunteerRequestId} the application has been set to the status of corrections",
            command.VolunteerRequestId);

        return UnitResult.Success<ErrorList>();
    }
}
