using NaReta.Application.UseCases.Category._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
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

    public async Task<OutputCategory> ExecuteAsync(InputCategory input)
    {
        var exists = await _repository.ExistsByNameAsync(input.Title);
        if (exists)
            throw new Exception(ResourceErrorMessages.CATEGORY_NAME_EXISTS);

        var category = new DomainEntity.Category(input.Name);
        await _repository.AddAsync(category);
        await _unitOfWork.Commit();

        return new OutputCategory
    private async Task ValidateAsync(InputCategory input)
        {
        var validator = new CategoryValidator();
        var result = validator.Validate(input);

        var categoryExists = await _repository.ExistsByNameAsync(input.Name);
        if (categoryExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.CATEGORY_NAME_EXISTS));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
