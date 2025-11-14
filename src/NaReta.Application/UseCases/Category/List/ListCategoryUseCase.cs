using NaReta.Application.UseCases.Category._Common;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.Application.UseCases.Category.List;
internal class ListCategoryUseCase : IListCategoryUseCase
{
    private readonly ICategoryReadOnlyRepository _repository;

    public ListCategoryUseCase(ICategoryReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OutputCategory>> ExecuteAsync()
    {
        var categories = await _repository.ListAsync();

        // [TODO] Mapper
        var output = new List<OutputCategory>();
        foreach (var category in categories)
        {
            output.Add(new OutputCategory
            {
                Id = category.Id,
                Name = category.Name
            });
        }
        
        return output;
    }
}
