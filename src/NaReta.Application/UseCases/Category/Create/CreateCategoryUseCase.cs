using NaReta.Application.UseCases.Category._Common;
using NaReta.Common;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Categories;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Category.Create;

internal class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryUseCase(ICategoryWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputCategory> ExecuteAsync(InputCreateCategory input)
    {
        var exists = await _repository.ExistsByNameAsync(input.Title);
        if (exists)
            throw new Exception(ResourceErrorMessages.CATEGORY_NAME_EXISTS);

        var category = new DomainEntity.Category(input.Title);
        await _repository.AddAsync(category);
        await _unitOfWork.Commit();

        return new OutputCategory
        {
            Id = category.Id,
            Name = category.Name,
        };
    }
}
