namespace ForgeCore.Inventories.Contracts.Responses
{
    public class InventoryResponse
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<InventoryEntryResponse> Entries { get; set; } = new();
    }

}
