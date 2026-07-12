namespace ForgeCore.Players.Domain
{
    public class Player
    {
        public Guid Id { get; private set; }

        public Guid AccountId { get; private set; }

        public string Nickname { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime LastActiveAt { get; private set; }

        public byte[] RowVersion { get; set; }

        public Player(Guid accountId, string nickname)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Nickname = nickname;

            CreatedAt = DateTime.UtcNow;
            LastActiveAt = CreatedAt;
        }

        public void Rename(string newName)
        {
            Nickname = newName;
            Touch();
        }

        public void Touch()
        {
            LastActiveAt = DateTime.UtcNow;
        }
    }
}
