using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Convertors;
using PetFamily.Core.Dto;
using PetFamily.Core.Dto.VolunteerRequest;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Read;

public class VolunteerRequestDtoConfiguration : IEntityTypeConfiguration<VolunteerRequestDto>
{
    public void Configure(EntityTypeBuilder<VolunteerRequestDto> builder)
    {
        builder.ToTable("volunteer_requests");

        builder.HasKey(p => p.Id)
            .HasName("id");

        builder.Property(v => v.RejectionDescription)
            .HasColumnName("rejection_description");

        builder.Property(v => v.UserId)
            .HasColumnName("user_id");
        
        builder.Property(p => p.InspectorId)
            .HasColumnName("inspector_id");

        builder.OwnsOne(v => v.Information, vb =>
        {
            vb.OwnsOne(vi => vi.FullName, vib =>
            {
                vib.Property(v => v.Name)
                    .IsRequired()
                    .HasColumnName("name");

                vib.Property(v => v.Surname)
                    .IsRequired()
                    .HasColumnName("surname");

                vib.Property(v => v.Patronymic)
                    .IsRequired(false)
                    .HasColumnName("patronymic");
            });

            vb.Property(vi => vi.Description)
                .HasColumnName("volunteer_description")
                .IsRequired();

            vb.Property(vi => vi.AgeExperience)
                .HasColumnName("years")
                .IsRequired();

            vb.Property(vi => vi.PhoneNumber)
                .HasColumnName("number")
                .IsRequired();
            
            
            vb.Property(v => v.Requisites)
                .HasConversion(u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                    json =>
                        JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!,
                    ValueComparerConvertor.CreateValueComparer<RequisiteDto>())
                .HasColumnName("requisites");
        });
    }
}
