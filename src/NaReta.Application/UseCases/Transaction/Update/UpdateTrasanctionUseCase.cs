using AutoMapper;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;

namespace NaReta.Application.UseCases.Transaction.Update;

internal class UpdateTrasanctionUseCase : IUpdateTrasanctionUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTrasanctionUseCase(
       IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OutputTransaction> ExecuteAsync(int id, InputTransaction input)
    {
        var transaction = await _unitOfWork.TransactionWriteOnlyRepository.GetByIdAsync(id);
        if (transaction is null)
            throw new NotFoundException(ResourceErrorMessages.TRANSACTION_NOT_FOUND);

        Validate(input);

        var category = await _unitOfWork.CategoryWriteOnlyRepository.GetByIdAsync(input.CategoryId);
        if (category is null)
            throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        transaction.ApplyChanges(input.Type, input.Amount, input.Date, category, input.Description);

        _unitOfWork.TransactionWriteOnlyRepository.Update(transaction);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<OutputTransaction>(transaction);
    }

    private void Validate(InputTransaction input)
    {
        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
