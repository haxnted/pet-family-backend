using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetFamily.Accounts.Application;
using PetFamily.Core.Dto.Accounts;

namespace PetFamily.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext(IConfiguration configuration) : DbContext, IAccountsReadDbContext
{
    private const string DATABASE = "PetFamilyDatabase";

    public IQueryable<UserDto> Users => Set<UserDto>();
    public IQueryable<AdminAccountDto> Admins => Set<AdminAccountDto>();
    public IQueryable<ParticipantAccountDto> Participants => Set<ParticipantAccountDto>();
    public IQueryable<VolunteerAccountDto> Volunteers => Set<VolunteerAccountDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("accounts")
            .ApplyConfigurationsFromAssembly(typeof(AccountsReadDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseSnakeCaseNamingConvention();
    }
}
