using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.TypeAccounts;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure.Configurations;

public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");
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
    }
}
