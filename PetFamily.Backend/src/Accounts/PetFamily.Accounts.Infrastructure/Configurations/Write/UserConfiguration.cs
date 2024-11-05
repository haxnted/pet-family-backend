using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Convertors;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(v => v.SocialLinks)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialLink>>(json, JsonSerializerOptions.Default)!,
                ValueComparerConvertor.CreateValueComparer<SocialLink>())
            .HasColumnName("social_links");
            
        builder.HasMany(u => u.Roles)
            .WithMany(u=> u.Users);

        builder.ComplexProperty(pa => pa.FullName, pab =>
        {
            pab.Property(f => f.Name)
                .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                .HasColumnName("name");
            pab.Property(f => f.Surname)
                .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                .HasColumnName("surname");
            pab.Property(f => f.Patronymic)
                .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                .HasColumnName("patronymic");
        });
    }
}
