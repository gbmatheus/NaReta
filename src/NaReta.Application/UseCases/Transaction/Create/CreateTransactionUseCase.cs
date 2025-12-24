using AutoMapper;
using NaReta.Application.UseCases.Transaction;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Application.UseCases.Transaction.Create;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Transactions.Create;

public class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransactionUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OutputTransaction> ExecuteAsync(int accountId, InputTransaction input)
    {
        var account = await _unitOfWork.AccountWriteOnlyRepository.GetByIdAsync(accountId);
        if (account is null)
            throw new NotFoundException(ResourceErrorMessages.ACCOUNT_NOT_FOUND);

        await ValidateAsync(input);

        var category = await _unitOfWork.CategoryWriteOnlyRepository.GetByIdAsync(input.CategoryId);
        if (category is null)
            throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        var transaction = new DomainEntity.Transaction(account, input.Type, input.Amount, input.Date, category, input.Description);
        await _unitOfWork.TransactionWriteOnlyRepository.AddAsync(transaction);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<OutputTransaction>(transaction);
    }

    private async Task ValidateAsync(InputTransaction input)
    {
        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        //var category = await _categoryRepository.FindByIdAsync(input.CategoryId);
        //if (category is null)
        //    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.CATEGORY_NOT_FOUND));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
