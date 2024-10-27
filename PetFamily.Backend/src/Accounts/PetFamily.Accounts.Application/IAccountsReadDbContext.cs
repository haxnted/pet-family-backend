using PetFamily.Core.Dto.Accounts;

namespace PetFamily.Accounts.Application;

public interface IAccountsReadDbContext
{
    public IQueryable<UserDto> Users { get; }
    public IQueryable<AdminAccountDto> Admins { get; }
    public IQueryable<ParticipantAccountDto> Participants { get; }
    public IQueryable<VolunteerAccountDto> Volunteers { get; }
}
