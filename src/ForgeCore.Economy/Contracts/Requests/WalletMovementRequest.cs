namespace ForgeCore.Economy.Contracts.Requests
{
    public class WalletMovementRequest
    {
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
