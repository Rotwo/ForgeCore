using ForgeCore.Auth.Domain;

namespace ForgeCore.Auth.Contracts
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<Account?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
