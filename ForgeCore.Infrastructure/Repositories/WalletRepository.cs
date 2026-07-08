using ForgeCore.Economy.Contracts;
using ForgeCore.Economy.Domain;
using ForgeCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ForgeCoreDbContext _db;

        public WalletRepository(ForgeCoreDbContext db)
        {
            _db = db;
        }

        public Task CreateWalletAsync(Wallet wallet)
        {
            _db.Wallets.Add(wallet);
            return _db.SaveChangesAsync();
        }

        public Task<Wallet?> GetWalletAsync(Guid walletId)
        {
            return _db.Wallets
                .Include(w => w.Balances)
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

        public Task UpdateWalletAsync(Wallet wallet)
        {
            _db.Wallets.Update(wallet);
            return _db.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }

        public Task<IEnumerable<Wallet>> GetByOwnerIdAsync(Guid ownerId)
        {
            var wallets = _db.Wallets
                .Include(w => w.Balances)
                .Where(w => w.OwnerId == ownerId);
            return Task.FromResult(wallets.AsEnumerable());
        }
    }
}