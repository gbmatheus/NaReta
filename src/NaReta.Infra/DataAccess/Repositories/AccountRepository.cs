using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Accounts;

namespace NaReta.Infra.DataAccess.Repositories;
internal class AccountRepository : IAccountReadOnlyRepository, IAccountWriteOnlyRepository
{
    private readonly NaRetaDBContext _dbContext;

    public AccountRepository(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Account account)
    {
        await _dbContext.accounts.AddAsync(account);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var account = await _dbContext.accounts.FirstOrDefaultAsync(a => a.Name == name);
        return account != null;
    }

    public async Task<Account?> FindByIdAsync(int id)
    {
        return await _dbContext.accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Account>> ListAsync()
    {
        return await _dbContext.accounts.AsNoTracking().ToListAsync();
    }
}
