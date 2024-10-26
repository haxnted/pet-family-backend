using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dto.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read;

public class AdminAccountDtoConfiguration : IEntityTypeConfiguration<AdminAccountDto>
{
    public void Configure(EntityTypeBuilder<AdminAccountDto> builder)
    {
        builder.ToTable("admin_accounts");
        builder.Property(v => v.Id)
            .HasColumnName("id");
        
        builder.Property(v => v.UserId)
            .HasColumnName("user_id");
    }
}
