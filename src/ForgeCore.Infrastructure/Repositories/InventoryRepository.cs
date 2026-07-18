using ForgeCore.Infrastructure.Persistence;
using ForgeCore.Inventories.Contracts;
using ForgeCore.Inventories.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ForgeCoreDbContext _db;

        public InventoryRepository(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public void Add(Inventory inventory)
        {
            _db.Inventories.Add(inventory);
        }

        public Task<Inventory?> GetByIdAsync(Guid inventoryId)
        {
            return _db.Inventories
                .Include(i => i.Entries)
                .FirstOrDefaultAsync(i => i.Id == inventoryId);
        }

        public Task<Inventory?> GetByOwnerIdAsync(Guid ownerId)
        {
            return _db.Inventories
                .Include(i => i.Entries)
                .FirstOrDefaultAsync(i => i.OwnerId == ownerId);
        }

        public async Task RemoveById(Guid inventoryId)
        {
            var inventory = await _db.Inventories
                .FirstOrDefaultAsync(i => i.Id == inventoryId);

            if (inventory is null)
                return;

            _db.Inventories.Remove(inventory);
        }
    }
}
