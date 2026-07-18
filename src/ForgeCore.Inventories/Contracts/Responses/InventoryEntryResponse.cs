namespace ForgeCore.Inventories.Contracts.Responses
{
    public class InventoryEntryResponse
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; }
        public string ItemKey { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int? SlotIndex { get; set; }
        public bool IsStackable { get; set; }
        public Dictionary<string, object?> Metadata { get; set; } = new();
    }

}
