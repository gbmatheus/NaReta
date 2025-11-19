using NaReta.Application.UseCases.Category._Common;

namespace NaReta.Application.UseCases.Category.Create;
public interface ICreateCategoryUseCase
{
    Task<OutputCategory> ExecuteAsync(InputCategory category);
}
