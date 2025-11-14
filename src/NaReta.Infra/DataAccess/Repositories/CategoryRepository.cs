using Microsoft.EntityFrameworkCore;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.Infra.DataAccess.Repositories;
internal class CategoryRepository : ICategoryReadOnlyRepository, ICategoryWriteOnlyRepository
{
    private readonly NaRetaDBContext _dbContext;

    public CategoryRepository(NaRetaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Category category)
    {
        await _dbContext.categories.AddAsync(category);
    }

    public async Task<bool> ExistsByNameAsync(string Name)
    {
        var transaction = await _dbContext.categories.FirstOrDefaultAsync(c => c.Name == Name);
        if (transaction is null)
            return false;
        return true;
    }

    public async Task<Category?> FindByIdAsync(int id)
    {
        return await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> ListAsync()
    {
        return await _dbContext.categories.AsNoTracking().ToListAsync();
    }

    public void Update(Category category)
    {
        _dbContext.categories.Update(category);
    }
}
