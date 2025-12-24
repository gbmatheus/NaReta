using NaReta.Domain.Entities;

namespace NaReta.Domain.Repositories.Categories;
public interface ICategoryWriteOnlyRepository: IBaseWriteOnlyRepository<Category>
{
    Task<bool> ExistsByNameAsync(string Name);
}
