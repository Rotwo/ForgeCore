using ForgeCore.Economy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("wallets");

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.OwnerId)
                .HasColumnName("owner_id")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.HasMany(x => x.Balances)
                .WithOne(x => x.Wallet)
                .HasForeignKey(x => x.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.RowVersion)
                .IsRowVersion()
                .HasColumnName("row_version");
        }
    }
}