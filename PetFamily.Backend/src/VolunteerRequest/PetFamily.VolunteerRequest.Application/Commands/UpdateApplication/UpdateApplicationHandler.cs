using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Application.Commands.UpdateApplication;

public class UpdateApplicationHandler(
    IValidator<UpdateApplicationCommand> validator,
    IUserRestrictionRepository userRestrictionRepository,
    IVolunteerRequestRepository volunteerRequestRepository,
    IVolunteerRequestUnitOfWork unitOfWork,
    ILogger<UpdateApplicationHandler> logger) : ICommandHandler<UpdateApplicationCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        UpdateApplicationCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var isUserBanned = await userRestrictionRepository.GetByUserId(command.ParticipantId, cancellationToken);
        if (isUserBanned.IsSuccess && isUserBanned.Value.IsBanActive())
            return Errors.UserRestriction.AccessDeniedForBannedUser(command.ParticipantId).ToErrorList();
        

        var volunteerRequest = await volunteerRequestRepository.GetById(command.VolunteerRequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var newVolunteerInformation = CreateVolunteerInformation(command);

        var result = volunteerRequest.Value.UpdateVolunteerInformation(newVolunteerInformation);
        if (result.IsFailure)
            return result.Error.ToErrorList();
        
        await unitOfWork.SaveChanges(cancellationToken);

        logger.Log(LogLevel.Information, 
            "VolunteerInformation updated. {VolunteerRequestId}",
            command.VolunteerRequestId);
        
        return UnitResult.Success<ErrorList>();
    }

    private VolunteerInformation CreateVolunteerInformation(UpdateApplicationCommand command)
    {
        var fullName = FullName.Create(command.VolunteerInformation.FullName.Name,
            command.VolunteerInformation.FullName.Surname, command.VolunteerInformation.FullName.Patronymic).Value;
        var ageExperience = AgeExperience.Create(command.VolunteerInformation.AgeExperience).Value;
        var phoneNumber = PhoneNumber.Create(command.VolunteerInformation.PhoneNumber).Value;
        var description = Description.Create(command.VolunteerInformation.Description).Value;
        var requisites = command.VolunteerInformation.Requisites
            .Select(s => Requisite.Create(s.Name, s.Description).Value)
            .ToList();

        return new VolunteerInformation(fullName, ageExperience, phoneNumber, description, requisites);
    }
}
