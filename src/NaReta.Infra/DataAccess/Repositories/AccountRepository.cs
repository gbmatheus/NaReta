using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Accounts;

namespace NaReta.Infra.DataAccess.Repositories;

internal class AccountRepository : BaseRepository<Account>, IAccountReadOnlyRepository, IAccountWriteOnlyRepository
{
    public AccountRepository(NaRetaDBContext dbContext) : base(dbContext) { }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var account = await GetAsync(a => a.Name == name);
        return account != null;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var account = await GetAsync(a => a.Email == email);
        return account != null;
    }

    async Task<Account?> IAccountReadOnlyRepository.FindByIdAsync(int id)
    {
        return await _dbContext.accounts.AsNoTracking()
            .Include(a => a.Transactions)
            .ThenInclude(t => t.Category)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Account>> ListAsync()
    {
        return await GetAll().AsNoTracking().ToListAsync();
    }
}
