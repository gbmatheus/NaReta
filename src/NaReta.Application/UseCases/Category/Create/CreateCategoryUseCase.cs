using AutoMapper;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Categories;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Category.Create;

internal class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OutputCategory> ExecuteAsync(InputCategory input)
    {
        await ValidateAsync(input);

        var category = new DomainEntity.Category(input.Name);
        await _unitOfWork.CategoryWriteOnlyRepository.AddAsync(category);
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
