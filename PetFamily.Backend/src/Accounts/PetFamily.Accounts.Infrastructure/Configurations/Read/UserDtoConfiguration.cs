using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Convertors;
using PetFamily.Core.Dto;
using PetFamily.Core.Dto.Accounts;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read;

public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(v => v.Id)
            .HasName("id");
        
        builder.Property(u => u.RoleId)
            .HasColumnName("role_id");
        
        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number");
        
        builder.ComplexProperty(pa => pa.FullName, pab =>
        {
            pab.Property(u => u.Name)
                .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                .HasColumnName("name");
            pab.Property(u => u.Surname)
                .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                .HasColumnName("surname");
            pab.Property(u => u.Patronymic)
                .HasMaxLength(Constants.MIN_TEXT_LENGTH)
                .HasColumnName("patronymic");
        });
        
        builder.Property(u => u.UserName)
            .HasColumnName("user_name");
        
        builder.Property(p => p.SocialLinks)
            .HasConversion(
                values => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialLinkDto>>(json, JsonSerializerOptions.Default)!,
                ValueComparerConvertor.CreateValueComparer<SocialLinkDto>());
        
        builder.HasOne(u => u.AdminAccount)
            .WithOne()
            .HasForeignKey<AdminAccountDto>(a => a.UserId);

        builder.HasOne(u => u.ParticipantAccount)
            .WithOne()
            .HasForeignKey<ParticipantAccountDto>(p => p.UserId);

        builder.HasOne(u => u.VolunteerAccount)
            .WithOne()
            .HasForeignKey<VolunteerAccountDto>(v => v.UserId);
            
    }
}
