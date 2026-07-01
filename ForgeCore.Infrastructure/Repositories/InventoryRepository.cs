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

        public async Task AddAsync(Inventory inventory)
        {
            await _db.Inventories.AddAsync(inventory);
            await _db.SaveChangesAsync();
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

        public async Task DeleteAsync(Guid inventoryId)
        {
            var inventory = await _db.Inventories
                .FirstOrDefaultAsync(i => i.Id == inventoryId);

            if (inventory is null)
                return;

            _db.Inventories.Remove(inventory);
            await _db.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
