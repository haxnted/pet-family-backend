using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerFamily.Discussion.Domain.Entities;
using PerFamily.Discussion.Domain.EntityIds;
using PetFamily.SharedKernel;

namespace PerFamily.Discussion.Infrastructure.Configurations.Write;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(id => id.Id,
                value => MessageId.Create(value));
        
        builder.ComplexProperty(v => v.Text, vb =>
        {
            vb.Property(d => d.Value)
                .HasColumnName("text")
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);
        });
    }
}
