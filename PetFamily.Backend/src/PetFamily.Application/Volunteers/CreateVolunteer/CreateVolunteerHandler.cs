﻿using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Models;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler(IVolunteersRepository repository)
{
    public async Task<Result<Guid, Error>> Execute(
        CreateVolunteerRequest request, CancellationToken token = default
    )
    {
        var phoneNumber = PhoneNumber.Create(request.Number);
        if (phoneNumber.IsFailure)
            return phoneNumber.Error;
        
        var volunteer = repository.GetByPhoneNumber(phoneNumber.Value);

        if (volunteer.Result.IsSuccess)
            return Errors.Model.AlreadyExist("Volunteer");
        
        var volunteerId = VolunteerId.NewId();
        
        var fullName = FullName.Create(request.Name, request.Surname, request.Patronymic);
        if (fullName.IsFailure)
            return fullName.Error;
        
        var description = Description.Create(request.Description);
        if (description.IsFailure)
            return description.Error;
        
        var ageExperience = AgeExperience.Create(request.AgeExperience);
        if (ageExperience.IsFailure)
            return ageExperience.Error;
        
        
        var volunteerResult = Volunteer.Create(volunteerId, 
            fullName.Value, description.Value, 
            ageExperience.Value, phoneNumber.Value, [], null);

        await repository.Add(volunteerResult.Value, token);
        
        return volunteerId.Id;
    }
}