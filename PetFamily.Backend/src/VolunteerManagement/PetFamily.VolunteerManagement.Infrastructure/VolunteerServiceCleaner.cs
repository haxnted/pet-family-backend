using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Domain;
using PetFamily.VolunteerManagement.Infrastructure.Options;

namespace PetFamily.VolunteerManagement.Infrastructure;

public class VolunteerServiceCleaner : IHardDeletableService
{
    private readonly VolunteersWriteDbContext _context;
    private readonly IOptions<SoftDeleteInfo> _options;
    private int DAYS_TO_DELETE => _options.Value.CountDaysBeforeHardDelete;

    public VolunteerServiceCleaner(
        VolunteersWriteDbContext context,
        IOptions<SoftDeleteInfo> options)
    {
        _context = context;
        _options = options;
    }

    public async Task Clean(CancellationToken cancellationToken)
    {
        var volunteers = await _context.Volunteers.Include(v => v.Pets).ToListAsync(cancellationToken);

        volunteers.ForEach(volunteer =>
        {
            DeleteVolunteerIfExpired(volunteer);
            DeletePetsIfExpired(volunteer);
        });
        await _context.SaveChangesAsync(cancellationToken);
    }

    private void DeleteVolunteerIfExpired(Volunteer volunteer)
    {
        if (volunteer.DeletedAt == null || volunteer.DeletedAt.Value.AddDays(DAYS_TO_DELETE) > DateTime.UtcNow) return;
        volunteer.HardRemoveAllPets();
        _context.Volunteers.Remove(volunteer);
    }

    private void DeletePetsIfExpired(Volunteer volunteer)
    {
        var petsTemp = volunteer.Pets.Where(pet =>
            pet.DeletedAt != null && pet.DeletedAt.Value.AddDays(DAYS_TO_DELETE) <= DateTime.UtcNow).ToList();
        foreach (var pet in petsTemp)
        {
            volunteer.HardRemovePet(pet);
        }
    }
}
