using ForgeCore.Economy.Contracts;
using ForgeCore.Economy.Domain;
using ForgeCore.Shared.Contracts;

namespace ForgeCore.Economy.Application
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Wallet> CreateWalletAsync(Guid ownerId)
        {
            var newWallet = new Wallet(ownerId: ownerId);
            _walletRepository.Add(newWallet);
            await _unitOfWork.SaveChangesAsync();
            return newWallet;
        }

        public async Task DepositAsync(Guid walletId, decimal amount, Guid currencyId)
        {
            var wallet = await _walletRepository.GetAsync(walletId);

            if (wallet is null)
                throw new InvalidOperationException($"Wallet with ID {walletId} not found.");

            wallet.Deposit(amount, currencyId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceAmountAsync(Guid walletId, Guid currencyId)
        {
            var wallet = await _walletRepository.GetAsync(walletId);
            if (wallet is null)
                throw new InvalidOperationException($"Wallet with ID {walletId} not found.");

            return wallet.Balances.FirstOrDefault(b => b.CurrencyId == currencyId)?.Balance ?? 0m;
        }

        public async Task<IEnumerable<CurrencyBalance>> GetBalancesAsync(Guid walletId)
        {
            var wallet = await _walletRepository.GetAsync(walletId);
            if (wallet is null)
                throw new InvalidOperationException($"Wallet with ID {walletId} not found.");

            return wallet.Balances;
        }

        public async Task<IEnumerable<CurrencyBalance>> GetBalancesByOwnerIdAsync(Guid ownerId)
        {
            var wallet = await _walletRepository.GetByOwnerIdAsync(ownerId);
            if (!wallet.Any())
                throw new InvalidOperationException($"Wallet with ID {ownerId} not found.");

            return wallet.SelectMany(w => w.Balances);
        }

        public Task<IEnumerable<Wallet>> GetWalletsByOwnerIdAsync(Guid ownerId)
        {
            var wallets = _walletRepository.GetByOwnerIdAsync(ownerId);
            return wallets;
        }

        public async Task WithdrawAsync(Guid walletId, decimal amount, Guid currencyId)
        {
            var wallet = await _walletRepository.GetAsync(walletId);

            if (wallet is null)
                throw new InvalidOperationException($"Wallet with ID {walletId} not found.");

            wallet.Withdraw(amount, currencyId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}