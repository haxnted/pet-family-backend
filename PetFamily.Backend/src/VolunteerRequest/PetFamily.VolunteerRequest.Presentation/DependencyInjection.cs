using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.VolunteerRequest.Application;
using PetFamily.VolunteerRequest.Contracts;
using PetFamily.VolunteerRequest.Infrastructure;
using PetFamily.VolunteerRequest.Presentation.Contracts;

namespace PetFamily.VolunteerRequest.Presentation;

public static class DependencyInjection
{
    public static void AddVolunteerRequestModule(this IServiceCollection collection)
    {
        collection.AddScoped<IVolunteerRequestContract, VolunteerRequestContract>()
            .AddVolunteerRequestApplication()
            .AddVolunteerRequestInfrastructure();
    }
}
