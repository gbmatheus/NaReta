using NaReta.Application.UseCases.Transaction;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Application.UseCases.Transaction.Create;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Transactions.Create;

internal class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly IAccountWriteOnlyRepository _accountRepository;
    private readonly ITransactionWriteOnlyRepository _transactionRepository;
    private readonly ICategoryWriteOnlyRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionUseCase(
        ITransactionWriteOnlyRepository transactionRepository,
        IAccountWriteOnlyRepository accountRepository,
        ICategoryWriteOnlyRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputTransaction> ExecuteAsync(int accountId, InputTransaction input)
    {
        var account = await _accountRepository.FindByIdAsync(accountId);
        if (account is null)
            throw new NotFoundException(ResourceErrorMessages.ACCOUNT_NOT_FOUND);

        await ValidateAsync(input);

        var category = await _categoryRepository.FindByIdAsync(input.CategoryId);
        if (category is null)
            throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        var transaction = new DomainEntity.Transaction(account, input.Type, input.Amount, input.Date, category, input.Description);
        await _transactionRepository.AddSync(transaction);
        await _unitOfWork.Commit();

        // [TODO] Mapper
        return new OutputTransaction
        {
            Id = transaction.Id,
            AccountId = accountId,
            Type = transaction.Type,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
            Category = transaction.Category.Name
        };
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
