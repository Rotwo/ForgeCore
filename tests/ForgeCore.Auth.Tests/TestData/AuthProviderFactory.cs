namespace ForgeCore.Auth.Tests.TestData
{
    public static class AuthProviderFactory
    {
        public static AuthProvider Create()
        {
            return new AuthProvider(type: AuthProviderType.EmailPassword, providerUserId: "example@test.com", email: "password123_");
        }
    }
}
