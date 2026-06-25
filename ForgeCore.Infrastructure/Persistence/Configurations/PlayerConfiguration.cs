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

            builder.HasKey(p => p.Id);

            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.DisplayName).HasColumnName("display_name");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        }
    }
}
