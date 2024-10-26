﻿namespace PetFamily.Core.Dto.Accounts;

public class UserDto
{
    public Guid Id { get; init; }
    public FullNameDto FullName { get; init; } = default!;
    public string UserName { get; init; } = string.Empty; 
    public string Email { get; init; } = string.Empty;
    public string? PhoneNumber { get; init; } = string.Empty;
    public Guid RoleId { get; init; }
    public string PhotoPath { get; init; } = string.Empty;
    public List<SocialLinkDto> SocialLinks { get; init; } = [];
    
    public AdminAccountDto? AdminAccountDto { get; set; }
    public ParticipantAccountDto? ParticipantAccountDto { get; set; }
    public VolunteerAccountDto? VolunteerAccountDto { get; set; }
}
