using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dto.Discussions;

namespace PerFamily.Discussion.Infrastructure.Configurations.Read;

public class MessageDtoConfiguration : IEntityTypeConfiguration<MessageDto>
{
    public void Configure(EntityTypeBuilder<MessageDto> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(v => v.Id)
            .HasName("id");
        
        builder.Property(v => v.Text)
            .HasColumnName("text");

        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(v => v.IsEdited)
            .HasColumnName("is_edited");
        
        builder.Property(v => v.DiscussionId)
            .HasColumnName("discussion_id");
        
    }
}
