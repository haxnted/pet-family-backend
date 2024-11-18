using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Commands.ExtendBanUser;

public class ExtendBanUserHandler(
    IValidator<ExtendBanUserCommand> validator,
    IUserRestrictionRepository banUserRepository,
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<ExtendBanUserHandler> logger) : ICommandHandler<ExtendBanUserCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        ExtendBanUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var banUser = await banUserRepository.GetByUserId(command.ParticipantId, cancellationToken);
        if (banUser.IsFailure)
            return Errors.General.NotFound(command.ParticipantId).ToErrorList();

        var result = banUser.Value.ExtendBan(command.AdditionalDays);
        if (result.IsFailure)
            return result.Error.ToErrorList();

        banUserRepository.Save(banUser.Value);
        await unitOfWork.SaveChanges(cancellationToken);

        logger.LogInformation("Ban for user {ParticipantId} has been extended by {AdditionalDays} days.", command.ParticipantId, command.AdditionalDays);

        return UnitResult.Success<ErrorList>();
    }
}