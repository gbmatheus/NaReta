
using AutoMapper;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.Application.UseCases.Category.Update;

internal class UpdateCategoryUseCase : IUpdateCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OutputCategory> ExecuteAsync(int id, InputCategory input)
    {
        await ValidateAsync(input);

        var category = await _unitOfWork.CategoryWriteOnlyRepository.GetByIdAsync(id);
        if (category is null)
            throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        category.ChangeName(input.Name);

        _unitOfWork.CategoryWriteOnlyRepository.Update(category);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<OutputCategory>(category);
    }

    private async Task ValidateAsync(InputCategory input)
    {
        var validator = new CategoryValidator();
        var result = validator.Validate(input);

        var categoryExists = await _unitOfWork.CategoryWriteOnlyRepository.ExistsByNameAsync(input.Name);
        if (categoryExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.CATEGORY_NAME_EXISTS));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
