namespace ForgeCore.Economy.Tests.TestData
{
    public static class WalletFactory
    {
        public static Wallet Create()
        {
            return new Wallet(Guid.NewGuid());
        }
    }
}
