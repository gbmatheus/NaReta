using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Transactions;
using NaReta.Domain.ValueObjects;

namespace NaReta.Infra.DataAccess.Repositories;

internal class TransactionRepository : BaseRepository<Transaction>, ITransactionReadOnlyRepository, ITransactionWriteOnlyRepository
{
    public TransactionRepository(NaRetaDBContext dbContext) : base(dbContext) { }

    public async Task<List<Transaction>> ListAsync()
    {
        return await GetAll().Include(t => t.Category).AsNoTracking().ToListAsync();
    }

    public async Task<List<Transaction>> ListByAccountIdAsync(
        int accountId,
        DateTime? startDate = null,
        DateTime? endDate = null,
        int pageNumber = 1,
        int itemPerPage = 10)
    {
        var query = GetAll().Include(t => t.Category).Where(t => t.Account.Id == accountId);

        if (startDate != null)
            query = query.Where(t => t.Date >= startDate);
        if (endDate != null)
            query = query.Where(t => t.Date <= endDate);

        query = query.Skip((pageNumber - 1) * itemPerPage).Take(itemPerPage);

        return await query.OrderBy(t => t.Date).AsNoTracking().ToListAsync();
    }

    public async Task<PagedResult<Transaction>> ListByTransactionFilterAsync(TransactionFilter filter)
    {
        var query = GetAll();
        if (filter.AccountId > 0)
            query = query.Where(t => t.Account.Id == filter.AccountId);
        if (filter.StartDate != null)
            query = query.Where(t => t.Date >= filter.StartDate);
        if (filter.EndDate != null)
            query = query.Where(t => t.Date <= filter.EndDate);

        var count = await query.CountAsync();
        var items = await query.Include(t => t.Category).OrderBy(t => t.Id).Skip(filter.PageSize * (filter.PageNumber - 1)).Take(filter.PageSize).ToListAsync();
        return new PagedResult<Transaction>(items, count, filter.PageNumber, filter.PageSize);
    }
}
