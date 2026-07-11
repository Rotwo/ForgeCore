using ForgeCore.Economy.Domain;

namespace ForgeCore.Economy.Contracts
{
    public interface IWalletRepository
    {
        void Add(Wallet wallet);
        void Update(Wallet wallet);
        Task<Wallet?> GetAsync(Guid walletId);
        Task<IEnumerable<Wallet>> GetByOwnerIdAsync(Guid ownerId);
        // Persist changes is handled by IUnitOfWork
    }
}