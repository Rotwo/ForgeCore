namespace ForgeCore.Economy.Domain
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }
        public byte[] RowVersion { get; set; }

        private readonly List<CurrencyBalance> _balances = new();
        public IReadOnlyCollection<CurrencyBalance> Balances => _balances;

        public void AddCurrencyBalance(CurrencyBalance balance)
        {
            if (_balances.Any(b => b.CurrencyId == balance.CurrencyId))
                throw new InvalidOperationException("Currency already exists.");

            _balances.Add(balance);
        }

        public void Deposit(decimal amount, Guid currencyId)
        {
            var balance = Balances.FirstOrDefault(x => x.CurrencyId == currencyId);

            if (balance is null)
            {
                balance = new CurrencyBalance(currencyId, Id);
                _balances.Add(balance);
            }

            balance.AddBalance(amount);
        }

        public void Withdraw(decimal amount, Guid currencyId)
        {
            var balance = Balances.FirstOrDefault(x => x.CurrencyId == currencyId);

            if (balance is null)
                throw new InvalidOperationException("Currency not found.");

            balance.SubtractBalance(amount);
        }

        public Wallet(Guid ownerId)
        {
            OwnerId = ownerId;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        private Wallet() { }
    }
}