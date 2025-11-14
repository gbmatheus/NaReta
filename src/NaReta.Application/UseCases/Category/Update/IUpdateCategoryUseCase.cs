using NaReta.Application.UseCases.Category._Common;

namespace NaReta.Application.UseCases.Category.Update;
public interface IUpdateCategoryUseCase
{
    Task<OutputCategory> ExecuteAsync(int id, InputUpdateCategory input);
}
