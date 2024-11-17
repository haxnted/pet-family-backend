using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Application.Commands.CreateApplication;

public class CreateApplicationHandler(
    IValidator<CreateApplicationCommand> validator,
    IUserRestrictionRepository userRestrictionRepository,
    IVolunteerRequestUnitOfWork unitOfWork,
    IVolunteerRequestRepository repository) : ICommandHandler<CreateApplicationCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        CreateApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var isUserBanned = await userRestrictionRepository.GetByUserId(command.ParticipantId, cancellationToken);
        if (isUserBanned.IsSuccess && isUserBanned.Value.GetRemainingBanTime() <= TimeSpan.Zero)
            return Errors.UserRestriction.AccessDeniedForBannedUser(command.ParticipantId).ToErrorList();
        
        var volunteerRequestId = VolunteerRequestId.NewId();
        var volunteerInformation = CreateVolunteerInformation(command);
        var volunteerRequest
            = Domain.VolunteerRequest.CreateRequest(volunteerRequestId, command.ParticipantId, volunteerInformation);

        await repository.Add(volunteerRequest, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }

    private VolunteerInformation CreateVolunteerInformation(CreateApplicationCommand command)
    {
        var fullName = FullName.Create(command.VolunteerInformation.FullName.Name,
            command.VolunteerInformation.FullName.Surname, command.VolunteerInformation.FullName.Patronymic).Value;
        var ageExperience = AgeExperience.Create(command.VolunteerInformation.AgeExperience).Value;
        var phoneNumber = PhoneNumber.Create(command.VolunteerInformation.PhoneNumber).Value;
        var description = Description.Create(command.VolunteerInformation.Description).Value;
        var requisites = command.VolunteerInformation.Requisites.Select(s => Requisite.Create(s.Name, s.Description).Value)
            .ToList();

        return new VolunteerInformation(fullName, ageExperience, phoneNumber, description, requisites);
    }
}
