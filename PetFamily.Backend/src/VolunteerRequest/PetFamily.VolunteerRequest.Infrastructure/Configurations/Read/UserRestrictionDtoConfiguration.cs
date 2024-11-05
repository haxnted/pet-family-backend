using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dto.VolunteerRequest;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Read;

public class UserRestrictionDtoConfiguration : IEntityTypeConfiguration<UserRestrictionDto> {
    public void Configure(EntityTypeBuilder<UserRestrictionDto> builder)
    {
        builder.ToTable("user_restrictions");

        builder.HasKey(u => u.Id)
            .HasName("id");

        builder.Property(u => u.UserId)
            .HasColumnName("user_id");
        
        builder.Property(u => u.BannedUntil)
            .HasColumnName("banned_until");
        
        builder.Property(u => u.Reason)
            .HasColumnName("reason_description");
    }
}
