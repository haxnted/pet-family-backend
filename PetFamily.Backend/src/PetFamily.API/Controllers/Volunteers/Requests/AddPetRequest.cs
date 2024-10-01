﻿using PetFamily.Application.Dto;
using PetFamily.Application.Features.VolunteerManagement.Commands.AddPet;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.API.Controllers.Volunteers;

public record AddPetRequest(
    string NickName,
    string GeneralDescription,
    string HealthDescription,
    AddressDto Address,
    double Weight,
    double Height,
    string PhoneNumber,
    DateTime BirthDate,
    Guid SpeciesId,
    Guid BreedId,
    bool IsCastrated,
    bool IsVaccinated,
    HelpStatusPet HelpStatus,
    IEnumerable<RequisiteDto> Requisites)
{
    public AddPetCommand ToCommand(Guid Volunteer)
    {
        return new AddPetCommand(Volunteer,
            NickName,
            GeneralDescription,
            HealthDescription,
            Address,
            Weight,
            Height,
            PhoneNumber,
            BirthDate,
            SpeciesId,
            BreedId,
            IsCastrated,
            IsVaccinated,
            HelpStatus,
            Requisites);
    }
}