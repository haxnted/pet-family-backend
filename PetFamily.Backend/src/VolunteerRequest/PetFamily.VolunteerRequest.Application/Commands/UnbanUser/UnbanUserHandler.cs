using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerRequest.Application.Commands.UnbanUser;

public class UnbanUserHandler(
    IUserRestrictionRepository banUserRepository,
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<UnbanUserHandler> logger) : ICommandHandler<UnbanUserCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        UnbanUserCommand command, CancellationToken cancellationToken = default)
    {
        var banUser = await banUserRepository.GetByUserId(command.ParticipantId, cancellationToken);
        if (banUser.IsFailure)
            return Errors.General.NotFound(command.ParticipantId).ToErrorList();

        var result = banUser.Value.EndBanManually();
        if (result.IsFailure)
            return result.Error.ToErrorList();

        banUserRepository.Save(banUser.Value);
        await unitOfWork.SaveChanges(cancellationToken);

        logger.LogInformation("User {ParticipantId} has been unbanned.", command.ParticipantId);

        return UnitResult.Success<ErrorList>();
    }
}