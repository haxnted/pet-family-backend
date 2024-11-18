using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Convertors;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.EntityIds;

namespace PetFamily.VolunteerRequest.Infrastructure.Configurations.Write;

public class VolunteerRequestConfiguration : IEntityTypeConfiguration<Domain.VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<Domain.VolunteerRequest> builder)
    {
        builder.ToTable("volunteer_requests");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Id,
                value => VolunteerRequestId.Create(value));

        builder.Property(p => p.InspectorId)
            .HasColumnName("inspector_id");

        builder.Property(v => v.UserId)
            .HasColumnName("user_id");
        
        builder.OwnsOne(v => v.RejectionDescription, vb =>
        {
            vb.Property(d => d.Value)
                .HasMaxLength(Constants.EXTRA_TEXT_LENGTH)
                .HasColumnName("rejection_description");
        });

        builder.OwnsOne(v => v.Information, vb =>
        {
            vb.OwnsOne(vi => vi.FullName, vib =>
            {
                vib.Property(v => v.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                    .HasColumnName("name");

                vib.Property(v => v.Surname)
                    .IsRequired()
                    .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                    .HasColumnName("surname");

                vib.Property(v => v.Patronymic)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                    .HasColumnName("patronymic");
            });
            
            vb.OwnsOne(vi => vi.Description, vib =>
            {
                vib.Property(d => d.Value)
                    .HasMaxLength(Constants.EXTRA_TEXT_LENGTH)
                    .HasColumnName("volunteer_description")
                    .IsRequired();
            });
            
            vb.OwnsOne(vi => vi.AgeExperience, vib =>
            {
                vib.Property(vi => vi.Years)
                    .IsRequired()
                    .HasColumnName("years");
            });

            vb.OwnsOne(vi => vi.PhoneNumber, vib =>
            {
                vib.Property(vi => vi.Value)
                    .IsRequired()
                    .HasColumnName("number");
            });
            
            vb.Property(v => v.Requisites)
                .HasConversion(
                    u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!,
                    ValueComparerConvertor.CreateValueComparer<Requisite>())
                .HasColumnName("requisites");
        });
    }
}
