using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.VolunteerRequest.Application;

namespace PetFamily.VolunteerRequest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestInfrastructure(this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddScoped<VolunteerRequestWriteDbContext>();
        collection.AddScoped<IVolunteerRequestUnitOfWork, VolunteerRequestUnitOfWork>();
        return collection;
    }
}