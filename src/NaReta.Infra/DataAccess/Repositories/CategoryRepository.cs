using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.Infra.DataAccess.Repositories;

internal class CategoryRepository : BaseRepository<Category>, ICategoryReadOnlyRepository, ICategoryWriteOnlyRepository
{
    public CategoryRepository(NaRetaDBContext dbContext) : base(dbContext) { }

    public async Task<bool> ExistsByNameAsync(string Name)
    {
        var transaction = await GetAsync(c => c.Name == Name);
        return transaction != null;
    }

    public async Task<List<Category>> ListAsync()
    {
        return await GetAll().AsNoTracking().ToListAsync();
    }
}
