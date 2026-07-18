namespace ForgeCore.Economy.Tests.TestData
{
    public static class CurrencyBalanceFactory
    {
        public static CurrencyBalance Create()
        {
            return new CurrencyBalance(Guid.NewGuid(), Guid.NewGuid());
        }

        public static CurrencyBalance CreateWithWalletId(Guid walletId)
        {
            return new CurrencyBalance(Guid.NewGuid(), walletId);
        }
    }
}
