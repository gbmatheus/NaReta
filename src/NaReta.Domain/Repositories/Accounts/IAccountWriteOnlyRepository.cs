using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Accounts;

public interface IAccountWriteOnlyRepository : IBaseWriteOnlyRepository<Account>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByEmailAsync(string email);
}
