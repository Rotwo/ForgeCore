using ForgeCore.Inventories.Contracts;
using ForgeCore.Inventories.Domain;

namespace ForgeCore.Inventories.Application
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task AddEntryAsync(Guid inventoryId, InventoryEntry entry)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory == null)
                throw new InvalidOperationException($"Inventory {inventoryId} not found.");

            inventory.AddEntry(entry);

            await _inventoryRepository.SaveChangesAsync();
        }

        public async Task<Inventory> CreateInventoryAsync(Guid ownerId)
        {
            var existing = await _inventoryRepository.GetByOwnerIdAsync(ownerId);

            if (existing is not null)
                throw new InvalidOperationException($"Owner {ownerId} already has an inventory.");

            var newInventory = new Inventory(ownerId);

            await _inventoryRepository.AddAsync(newInventory);

            return newInventory;
        }

        public async Task DeleteInventoryAsync(Guid inventoryId)
        {
            await _inventoryRepository.DeleteAsync(inventoryId);
        }

        public Task<Inventory?> GetInventoryAsync(Guid ownerId)
        {
            return _inventoryRepository.GetByOwnerIdAsync(ownerId);
        }

        public async Task RemoveEntryAsync(Guid inventoryId, Guid entryId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory == null)
                throw new InvalidOperationException($"Inventory {inventoryId} not found.");

            inventory.RemoveEntry(entryId);

            await _inventoryRepository.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(Guid inventoryId, InventoryEntry newEntry)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory == null)
                throw new InvalidOperationException($"Inventory {inventoryId} not found.");

            inventory.ModifyEntry(newEntry);

            await _inventoryRepository.SaveChangesAsync();
        }
    }
}