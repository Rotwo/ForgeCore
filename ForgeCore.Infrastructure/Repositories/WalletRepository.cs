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

        public void Add(Wallet wallet)
        {
            _db.Wallets.Add(wallet);
        }

        public Task<Wallet?> GetAsync(Guid walletId)
        {
            return _db.Wallets
                .Include(w => w.Balances)
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

        public void Update(Wallet wallet)
        {
            _db.Wallets.Update(wallet);
        }

        // Persistence is handled by UnitOfWork

        public Task<IEnumerable<Wallet>> GetByOwnerIdAsync(Guid ownerId)
        {
            var wallets = _db.Wallets
                .Include(w => w.Balances)
                .Where(w => w.OwnerId == ownerId);
            return Task.FromResult(wallets.AsEnumerable());
        }
    }
}