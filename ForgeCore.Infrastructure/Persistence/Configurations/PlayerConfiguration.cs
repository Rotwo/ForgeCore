using ForgeCore.Players.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");

            builder.HasIndex(p => p.Id)
                .IsUnique();

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(x => x.AccountId).HasColumnName("account_id").IsRequired();
            builder.Property(x => x.Nickname).HasColumnName("nickname").IsRequired();
            builder.Property(x => x.LastActiveAt).HasColumnName("last_active_at").IsRequired();
        }
    }
}
