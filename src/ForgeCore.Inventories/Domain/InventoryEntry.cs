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
        public InventoryEntryMetadata Metadata { get; private set; } = new();

        public byte[] RowVersion { get; set; }

        private InventoryEntry() { }

        public InventoryEntry(string itemKey, int quantity, int? slotIndex, bool isStackable)
        {
            Id = Guid.NewGuid();
            ItemKey = itemKey;
            Quantity = quantity;
            SlotIndex = slotIndex;
            IsStackable = isStackable;
        }

        public InventoryEntry(Guid id, string itemKey, int quantity, int? slotIndex, bool isStackable)
        {
            Id = id;
            ItemKey = itemKey;
            Quantity = quantity;
            SlotIndex = slotIndex;
            IsStackable = isStackable;
        }

        public void Update(InventoryEntry newEntry)
        {
            ItemKey = newEntry.ItemKey;
            Quantity = newEntry.Quantity;
            SlotIndex = newEntry.SlotIndex;
            IsStackable = newEntry.IsStackable;
            Metadata = newEntry.Metadata.Clone();
        }

        public void SetMetadata(string key, object? value)
        {
            Metadata.Set(key, value);
        }

        public void Increase(int amount)
        {
            if (!IsStackable)
                throw new InvalidOperationException("Cannot increase quantity of a non-stackable item.");
            Quantity += amount;
        }

        public void Decrease(int amount)
        {
            if (!IsStackable)
                throw new InvalidOperationException("Cannot decrease quantity of a non-stackable item.");
            if (Quantity - amount < 0)
                throw new InvalidOperationException("Cannot decrease quantity below zero.");
            Quantity -= amount;
        }
    }
}
