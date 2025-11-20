using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Accounts;

public interface IAccountWriteOnlyRepository
{
    Task AddAsync(Account account);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByEmailAsync(string email);
    Task<Account?> FindByIdAsync(int id);
}
