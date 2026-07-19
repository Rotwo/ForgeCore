namespace ForgeCore.Shared.Contracts
{
    public interface ICurrentUserService
    {
        Guid AccountId { get; }
        Guid SessionId { get; }
    }
}