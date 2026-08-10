using System.Text.Json.Nodes;

namespace ForgeCore.Inventories.Contracts.Requests
{
    public class UpdateEntryRequest
    {
        public Guid Id { get; set; }
        public string ItemKey { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int? SlotIndex { get; set; }
        public bool IsStackable { get; set; }
        public JsonObject? Metadata { get; set; }
    }

}
