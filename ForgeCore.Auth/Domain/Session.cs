namespace ForgeCore.Auth.Domain
{
    public class Session
    {
        public Guid Id { get; private set; }

        public Guid AccountId { get; private set; }

        public string RefreshToken { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public string? DeviceInfo { get; private set; }
        public string? IpAddress { get; private set; }

        private Session() { }

        public Session(Guid accountId, string refreshToken, TimeSpan duration)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            RefreshToken = refreshToken;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = CreatedAt.Add(duration);
        }

        public bool IsExpired()
            => DateTime.UtcNow > ExpiresAt;
    }
}
