namespace ForgeCore.Auth.Tests.TestData
{
    public static class AccountFactory
    {
        public static Account Create()
        {
            return new Account(email: "example@test.com", displayName: "player_123");
        }
    }
}
