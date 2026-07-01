using ForgeCore.Inventories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class InventoryEntryConfiguration : IEntityTypeConfiguration<InventoryEntry>
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);
        private static readonly ValueComparer<InventoryEntryMetadata> MetadataComparer = new(
            (left, right) => SerializeMetadata(left) == SerializeMetadata(right),
            value => SerializeMetadata(value).GetHashCode(),
            value => value == null ? new InventoryEntryMetadata() : value.Clone());

        public void Configure(EntityTypeBuilder<InventoryEntry> builder)
        {
            builder.ToTable("inventory_entries");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

            builder.Property(e => e.InventoryId)
                .HasColumnName("inventory_id")
                .IsRequired()
                .ValueGeneratedNever();

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
                .HasColumnType("jsonb")
                .HasConversion(
                    value => SerializeMetadata(value),
                    value => DeserializeMetadata(value))
                .Metadata.SetValueComparer(MetadataComparer);
        }

        private static string SerializeMetadata(InventoryEntryMetadata? metadata)
        {
            return JsonSerializer.Serialize(metadata?.ToDictionary() ?? [], JsonSerializerOptions);
        }

        private static InventoryEntryMetadata DeserializeMetadata(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new InventoryEntryMetadata();

            var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, JsonSerializerOptions);

            return InventoryEntryMetadata.From(values);
        }
    }
}
