using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Categories;
public interface ICategoryWriteOnlyRepository
{
    Task AddAsync(Category category);
    void Update(Category category);
    Task<bool> ExistsByNameAsync(string Name);
    Task<Category?> FindByIdAsync(int id);

}
