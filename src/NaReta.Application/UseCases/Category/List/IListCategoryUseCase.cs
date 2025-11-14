using NaReta.Application.UseCases.Category._Common;

namespace NaReta.Application.UseCases.Category.List;
public interface IListCategoryUseCase
{
    Task<List<OutputCategory>> ExecuteAsync();
}
