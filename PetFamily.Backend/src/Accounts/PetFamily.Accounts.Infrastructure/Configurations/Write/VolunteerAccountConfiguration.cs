using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Core.Convertors;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(v => v.Requisites)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!,
                ValueComparerConvertor.CreateValueComparer<Requisite>())
            .HasColumnName("requisites");
    }
}
