﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.Core.Convertors;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;


namespace PetFamily.Accounts.Infrastructure.Configurations;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.HasKey(b => b.Id);
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
        
        builder.Property(v => v.Requisites)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!,
                ValueComparerConvertor.CreateValueComparer<Requisite>())
            .HasColumnName("requisites");
    }
}
