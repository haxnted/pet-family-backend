﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Convertors;
using PetFamily.SharedKernel.ValueObjects;


namespace PetFamily.Accounts.Infrastructure.Configurations;

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
            
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}