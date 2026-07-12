using ForgeCore.Inventories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventories");

            builder.HasIndex(i => i.Id)
                .IsUnique();

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

            builder.Property(i => i.OwnerId)
                .HasColumnName("owner_id")
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.HasMany(i => i.Entries)
                .WithOne()
                .HasForeignKey(e => e.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(i => i.Entries)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(i => i.RowVersion)
                .IsRowVersion()
                .HasColumnName("row_version");
        }
    }
}
