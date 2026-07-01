using ForgeCore.Inventories.Domain;

namespace ForgeCore.Inventories.Contracts
{
    public interface IInventoryService
    {
        Task<Inventory> CreateInventoryAsync(Guid ownerId);
        Task<Inventory?> GetInventoryAsync(Guid ownerId);
        Task DeleteInventoryAsync(Guid inventoryId);
        Task AddEntryAsync(Guid inventoryId, InventoryEntry entry);
        Task RemoveEntryAsync(Guid inventoryId, Guid entryId);
        Task UpdateEntryAsync(Guid inventoryId, InventoryEntry newEntry);
    }
}
