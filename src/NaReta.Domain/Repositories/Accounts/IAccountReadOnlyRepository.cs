using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Accounts;

public interface IAccountReadOnlyRepository
{
    Task<List<Account>> ListAsync();
    Task<Account?> FindByIdAsync(int id);
}
