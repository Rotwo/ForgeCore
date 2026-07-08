using ForgeCore.Economy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    public class CurrencyBalanceConfiguration : IEntityTypeConfiguration<CurrencyBalance>
    {
        public void Configure(EntityTypeBuilder<CurrencyBalance> builder)
        {
            builder.ToTable("currency_balances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Balance)
                .HasPrecision(18, 2);

            builder.Property(x => x.CurrencyId)
                .IsRequired();
        }
    }
}