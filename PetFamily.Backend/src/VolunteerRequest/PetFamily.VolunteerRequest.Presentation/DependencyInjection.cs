using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Contracts;
using PetFamily.VolunteerRequest.Infrastructure;
using PetFamily.VolunteerRequest.Presentation.Contracts;

namespace PetFamily.VolunteerRequest.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestModule(
        this IServiceCollection collection, IConfiguration configuration)
    {
        return collection.AddScoped<IVolunteerRequestContract, VolunteerRequestContract>()
            .AddVolunteerRequestApplication()
            .AddVolunteerRequestInfrastructure(configuration);
    }
}
