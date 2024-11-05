using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.VolunteerRequest.Domain;

namespace PetFamily.VolunteerRequest.Infrastructure;

public class VolunteerRequestWriteDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = "PetFamilyDatabase";
    
    public DbSet<Domain.VolunteerRequest> VolunteerRequests => Set<Domain.VolunteerRequest>();
    public DbSet<UserRestriction> UserRestrictions => Set<UserRestriction>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("volunteer_requests");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerRequestWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging()
            .UseNpgsql(configuration.GetConnectionString(DATABASE));
    }

    private ILoggerFactory CreateLoggerFactory() =>
         LoggerFactory.Create(builder =>
             builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                .AddConsole());
    
}