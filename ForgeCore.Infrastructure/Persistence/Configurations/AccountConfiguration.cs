using ForgeCore.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.DisplayName)
                .HasColumnName("display_name")
                .IsRequired();

            builder.HasIndex(x => x.DisplayName)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired(false);  // Email nullable (guest accounts)

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            // Relación con AuthProviders
            builder.HasMany(a => a.AuthProviders)
                .WithOne()
                .HasForeignKey("AccountId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.RowVersion)
                .IsRowVersion()
                .HasColumnName("row_version");
        }
    }
}
