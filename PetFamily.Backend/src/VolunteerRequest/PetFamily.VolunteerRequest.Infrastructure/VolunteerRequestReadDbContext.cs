using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Dto.VolunteerRequest;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Infrastructure;

public class VolunteerRequestReadDbContext(IConfiguration configuration) : DbContext, IVolunteerRequestReadDbContext
{
    private const string DATABASE = "PetFamilyDatabase";

    public IQueryable<VolunteerRequestDto> VolunteerRequests => Set<VolunteerRequestDto>();
    public IQueryable<UserRestrictionDto> UserRestrictions => Set<UserRestrictionDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("volunteer_requests");
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerRequestReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging()
            .UseNpgsql(configuration.GetConnectionString(DATABASE))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                .AddConsole();
        });
    }
    
}