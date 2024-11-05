using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PerFamily.Discussion.Application;
using PetFamily.Core.Dto.Discussions;

namespace PerFamily.Discussion.Infrastructure;

public class DiscussionReadDbContext(IConfiguration configuration) : DbContext, IDiscussionReadDbContext
{
    private const string DATABASE = "PetFamilyDatabase";

    public IQueryable<DiscussionDto> Discussions => Set<DiscussionDto>();
    public IQueryable<MessageDto> Messages => Set<MessageDto>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("discussions");
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DiscussionReadDbContext).Assembly,
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