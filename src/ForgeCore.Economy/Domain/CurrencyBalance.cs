namespace ForgeCore.Economy.Domain
{
    public class CurrencyBalance
    {
        public Guid Id { get; private set; }

        public Guid CurrencyId { get; private set; }
        public Guid WalletId { get; private set; }
        public Wallet? Wallet { get; private set; }

        public decimal Balance { get; private set; }

        public byte[] RowVersion { get; set; }

        private CurrencyBalance() { }

        public CurrencyBalance(Guid currencyId, Guid walletId)
        {
            Id = Guid.NewGuid();
            CurrencyId = currencyId;
            WalletId = walletId;
        }

        public void AddBalance(decimal amount)
        {
            Balance += amount;
        }

        public void SubtractBalance(decimal amount)
        {
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient funds.");

            Balance -= amount;
        }
    }
}