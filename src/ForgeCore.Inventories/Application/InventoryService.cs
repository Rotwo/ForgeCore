using ForgeCore.Inventories.Contracts;
using ForgeCore.Inventories.Domain;
using ForgeCore.Shared.Contracts;

namespace ForgeCore.Inventories.Application
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddEntryAsync(Guid inventoryId, InventoryEntry entry)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory == null)
                throw new InvalidOperationException($"Inventory {inventoryId} not found.");

            inventory.AddEntry(entry);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ClearEntriesAsync(Guid inventoryId)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory is null)
                throw new InvalidOperationException($"Inventory {inventoryId} not found.");

            inventory.ClearEntries();
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Inventory> CreateInventoryAsync(Guid ownerId)
        {
            var existing = await _inventoryRepository.GetByOwnerIdAsync(ownerId);

            if (existing is not null)
                throw new InvalidOperationException($"Owner {ownerId} already has an inventory.");

            var newInventory = new Inventory(ownerId);

            _inventoryRepository.Add(newInventory);
            await _unitOfWork.SaveChangesAsync();

            return newInventory;
        }

        public async Task DeleteInventoryAsync(Guid inventoryId)
        {
            await _inventoryRepository.RemoveById(inventoryId);
            await _unitOfWork.SaveChangesAsync();
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

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(Guid inventoryId, Guid oldEntryId, InventoryEntry newEntry)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory == null)
                throw new InvalidOperationException($"Inventory {inventoryId} not found.");

            inventory.ModifyEntry(oldEntryId, newEntry);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}