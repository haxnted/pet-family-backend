using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.VolunteerRequest.Domain;
using PetFamily.VolunteerRequest.Domain.EntityIds;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Write;

public class UserRestrictionConfiguration : IEntityTypeConfiguration<UserRestriction> {
    public void Configure(EntityTypeBuilder<UserRestriction> builder)
    {
        builder.ToTable("user_restrictions");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Id,
                value => UserRestrictionId.Create(value));

        builder.Property(u => u.UserId)
            .HasColumnName("user_id");
        
        builder.Property(u => u.BannedUntil)
            .HasColumnName("banned_until");
        
        
        builder.ComplexProperty(u => u.Reason, ub =>
        {
            ub.Property( u => u.Value)
                .HasColumnName("reason_description");
        });
    }
}
