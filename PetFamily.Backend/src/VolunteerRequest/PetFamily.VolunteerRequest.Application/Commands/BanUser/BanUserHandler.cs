using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Contracts;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Application.Commands.BanUser;

public class BanUserHandler(
    IValidator<BanUserCommand> validator,
    IUserRestrictionRepository banUserRepository,
    IAccountContract contract, 
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<BanUserHandler> logger) : ICommandHandler<BanUserCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        BanUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var user = await contract.IsUserExists(command.ParticipantId, cancellationToken);
        if (user.IsFailure)
            return Errors.General.NotFound(command.ParticipantId).ToErrorList();

        var banDurationDays = command.BanDurationDays;
        var reason = RejectionDescription.Create(command.Reason).Value;

        var banUserId = UserRestrictionId.NewId();
        var banResult = Domain.UserRestriction.Create(banUserId, command.ParticipantId, reason, banDurationDays);
        if (banResult.IsFailure)
            return banResult.Error.ToErrorList();

        await banUserRepository.Add(banResult.Value, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        logger.LogInformation("User {ParticipantId} has been banned for {BanDurationDays} days. Reason: {Reason}", command.ParticipantId, banDurationDays, reason);

        return UnitResult.Success<ErrorList>();
    }
}