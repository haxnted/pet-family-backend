using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dto.Discussions;

namespace PerFamily.Discussion.Infrastructure.Configurations.Read;

public class DiscussionDtoConfiguration : IEntityTypeConfiguration<DiscussionDto>
{
    public void Configure(EntityTypeBuilder<DiscussionDto> builder)
    {
        builder.ToTable("discussions");

        builder.HasKey(v => v.Id);
     
        builder.Property(u => u.ParticipantId)
            .HasColumnName("userId")
            .IsRequired();
        
        builder.Property(u => u.AdminId)
            .HasColumnName("adminId")
            .IsRequired();
        
        builder.HasMany(d => d.Messages)
            .WithOne()
            .HasForeignKey(m => m.DiscussionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}