namespace ForgeCore.Auth.Domain
{
    public class AuthProvider
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }

        public AuthProviderType Type { get; private set; }

        public string ProviderUserId { get; private set; } // id in provider system (steam, google..)
        public string? Email { get; private set; }

        public DateTime LinkedAt { get; private set; }

        private AuthProvider() { }

        public AuthProvider(AuthProviderType type, string providerUserId, string? email = null)
        {
            Id = Guid.NewGuid();
            Type = type;
            ProviderUserId = providerUserId;
            Email = email;
            LinkedAt = DateTime.UtcNow;
        }
    }
}
