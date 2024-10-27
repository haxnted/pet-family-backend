using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Convertors;
using PetFamily.Core.Dto;
using PetFamily.Core.Dto.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read;

public class VolunteerAccountDtoConfiguration : IEntityTypeConfiguration<VolunteerAccountDto>
{
    public void Configure(EntityTypeBuilder<VolunteerAccountDto> builder)
    {
        builder.ToTable("volunteers");
        builder.Property(v => v.Id)
            .HasColumnName("id");

        builder.Property(v => v.Experience)
            .HasColumnName("experience");

        builder.Property(v => v.Requisites)
            .HasConversion(values => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!,
                ValueComparerConvertor.CreateValueComparer<RequisiteDto>());

        builder.Property(v => v.UserId)
            .HasColumnName("user_id");
    }
}