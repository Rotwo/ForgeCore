using ForgeCore.Inventories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class InventoryEntryConfiguration : IEntityTypeConfiguration<InventoryEntry>
    {
        public void Configure(EntityTypeBuilder<InventoryEntry> builder)
        {
            builder.ToTable("inventory_entries");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.InventoryId)
                .HasColumnName("inventory_id")
                .IsRequired();

            builder.Property(e => e.ItemKey)
                .HasColumnName("item_key")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(e => e.SlotIndex)
                .HasColumnName("slot_index");

            builder.Property(e => e.IsStackable)
                .HasColumnName("is_stackable")
                .IsRequired();

            builder.Property(e => e.Metadata)
                .HasColumnName("metadata")
                .HasColumnType("jsonb");
        }
    }
}