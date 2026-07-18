using ForgeCore.Economy.Domain;

namespace ForgeCore.Economy.Contracts
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletAsync(Guid ownerId);
        Task<IEnumerable<Wallet>> GetWalletsByOwnerIdAsync(Guid ownerId);

        Task<IEnumerable<CurrencyBalance>> GetBalancesAsync(Guid walletId);
        Task<IEnumerable<CurrencyBalance>> GetBalancesByOwnerIdAsync(Guid ownerId);
        Task<decimal> GetBalanceAmountAsync(Guid walletId, Guid currencyId);

        Task DepositAsync(Guid walletId, decimal amount, Guid currencyId);
        Task WithdrawAsync(Guid walletId, decimal amount, Guid currencyId);
    }
}