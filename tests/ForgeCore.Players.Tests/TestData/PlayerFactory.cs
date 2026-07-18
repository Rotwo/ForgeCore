namespace ForgeCore.Players.Tests.TestData
{
    public static class PlayerFactory
    {
        public static Player Create()
        {
            return new Player(accountId: Guid.NewGuid(), nickname: "Player");
        }
    }
}