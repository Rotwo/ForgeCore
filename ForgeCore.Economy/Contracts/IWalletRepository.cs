using ForgeCore.Economy.Domain;

namespace ForgeCore.Economy.Contracts
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetWalletAsync(Guid walletId);
        Task CreateWalletAsync(Wallet wallet);
        Task UpdateWalletAsync(Wallet wallet);
        Task<IEnumerable<Wallet>> GetByOwnerIdAsync(Guid ownerId);
        Task SaveChangesAsync();
    }
}