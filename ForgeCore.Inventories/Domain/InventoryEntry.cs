namespace ForgeCore.Inventories.Domain
{
    public class InventoryEntry
    {
        public Guid Id { get; private set; }
        public Guid InventoryId { get; private set; }
        public string ItemKey { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public int? SlotIndex { get; private set; }
        public bool IsStackable { get; private set; }
        public InventoryEntryMetadata Metadata { get; private set; }

        public void Update(InventoryEntry newEntry)
        {
            ItemKey = newEntry.ItemKey;
            Quantity = newEntry.Quantity;
            SlotIndex = newEntry.SlotIndex;
            IsStackable = newEntry.IsStackable;
            Metadata = newEntry.Metadata;
        }
    }
}
