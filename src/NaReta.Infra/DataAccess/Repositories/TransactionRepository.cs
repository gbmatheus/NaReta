using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Infra.DataAccess.Repositories;

internal class TransactionRepository : ITransactionReadOnlyRepository, ITransactionWriteOnlyRepository
{
    private readonly NaRetaDBContext _dbContext;

    public TransactionRepository(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSync(Transaction transaction)
    {
        await _dbContext.transactions.AddAsync(transaction);
    }

    public async Task<Transaction?> FindByIdAsync(int id)
    {
        return await _dbContext.transactions.FindAsync(id);
    }

    public async Task<List<Transaction>> ListAsync()
    {
        return await _dbContext.transactions.Include(t => t.Category).AsNoTracking().ToListAsync();
    }

    public async Task<List<Transaction>> ListByAccountIdAsync(
        int accountId,
        DateTime? startDate = null,
        DateTime? endDate = null,
        int pageNumber = 1,
        int itemPerPage = 10)
    {
        var query = _dbContext.transactions.Include(t => t.Category).Where(t => t.Account.Id == accountId).AsQueryable();

        if (startDate != null)
            query = query.Where(t => t.Date >= startDate);
        if (endDate != null)
            query = query.Where(t => t.Date <= endDate);

        query = query.Skip((pageNumber - 1) * itemPerPage).Take(itemPerPage);

        return await query.OrderBy(t => t.Date).AsNoTracking().ToListAsync();
    }

    public void Remove(Transaction transaction)
    {
        _dbContext.transactions.Remove(transaction);
    }

    public void Update(Transaction transaction)
    {
        _dbContext.transactions.Update(transaction);
    }
}
