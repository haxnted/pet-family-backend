﻿using PetFamily.Application.Dto;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Description,
    int AgeExperience,
    string Number,
    IEnumerable<SocialLinkDto> SocialLinks,
    IEnumerable<RequisiteDto> Requisites);