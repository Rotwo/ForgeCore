namespace ForgeCore.Auth.Tests.TestData
{
    public static class SessionFactory
    {
        public static Session Create()
        {
            return new Session(accountId: Guid.NewGuid(), refreshToken: "refresh-token", duration: TimeSpan.FromDays(7));
        }
    }
}
