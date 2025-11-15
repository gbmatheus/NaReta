
using NaReta.Application.UseCases.Category._Common;
using NaReta.Common;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.Application.UseCases.Category.Update;

internal class UpdateCategoryUseCase : IUpdateCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryUseCase(ICategoryReadOnlyRepository readOnlyRepository, ICategoryWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputCategory> ExecuteAsync(int id, InputUpdateCategory input)
    {
        var category = await _repository.FindByIdAsync(id);
        if (category is null)
            // NotFound
            throw new Exception(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        var categoryExists = await _repository.ExistsByNameAsync(input.Name);
        if (categoryExists)
            // BadRequest
            throw new Exception(ResourceErrorMessages.CATEGORY_NAME_EXISTS);

        category.ChangeName(input.Name);

        _repository.Update(category);
        await _unitOfWork.Commit();

        // [TODO] Mapper
        return new OutputCategory
        {
            Id = category.Id,
            Name = category.Name,
        };
    }
}
