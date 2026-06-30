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

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasColumnName("id");

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
        }
    }
}