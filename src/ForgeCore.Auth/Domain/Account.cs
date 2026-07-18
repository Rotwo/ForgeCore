namespace ForgeCore.Auth.Domain
{
    public class Account
    {
        public Guid Id { get; private set; }

        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<AuthProvider> _authProviders = new();
        public IReadOnlyCollection<AuthProvider> AuthProviders => _authProviders;

        public byte[] RowVersion { get; set; }

        private Account() { }

        public Account(string email, string displayName)
        {
            Id = Guid.NewGuid();
            Email = email;
            DisplayName = displayName;
            CreatedAt = DateTime.UtcNow;
        }

        public void LinkProvider(AuthProvider provider)
        {
            _authProviders.Add(provider);
        }
    }
}
