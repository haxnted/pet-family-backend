﻿using PetFamily.Application.Dto;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Description,
    int AgeExperience,
    string Number,
    IEnumerable<SocialLinkDto> SocialLinks,
    IEnumerable<RequisiteDto> Requisites)
{
    public CreateVolunteerCommand ToCommand() =>
        new(
            FullName,
            Description,
            AgeExperience,
            Number,
            SocialLinks,
            Requisites);
}