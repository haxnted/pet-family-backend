using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerFamily.Discussion.Domain.EntityIds;

namespace PerFamily.Discussion.Infrastructure.Configurations.Write;

public class DiscussionConfiguration : IEntityTypeConfiguration<Domain.Discussion>
{
    public void Configure(EntityTypeBuilder<Domain.Discussion> builder)
    {
        builder.ToTable("discussions");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(id => id.Id,
                value => DiscussionId.Create(value));

        builder.ComplexProperty(d => d.Users, db =>
        {
            db.Property(u => u.UserId)
                .HasColumnName("userId")
                .IsRequired();
            
            db.Property(u => u.UserId)
                .HasColumnName("adminId")
                .IsRequired();
        });
        
        builder.HasMany(d => d.Messages)
            .WithOne()
            .HasForeignKey(m => m.DiscussionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}