using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Infrastructure.Repositories;

namespace PetFamily.VolunteerRequest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestInfrastructure(this IServiceCollection collection)
    {
        
        return collection.AddScoped<VolunteerRequestWriteDbContext>()
        .AddScoped<IVolunteerRequestUnitOfWork, VolunteerRequestUnitOfWork>()
        .AddScoped<IVolunteerRequestReadDbContext, VolunteerRequestReadDbContext>()
        .AddScoped<IUserRestrictionRepository, UserRestrictionRepository>()
        .AddScoped<IVolunteerRequestRepository, VolunteerRequestRepository>();

    }
}