using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Transaction.Update;

internal class UpdateTrasanctionUseCase : IUpdateTrasanctionUseCase
{
    private readonly ITransactionWriteOnlyRepository _transactionRepository;
    private readonly ICategoryWriteOnlyRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTrasanctionUseCase(
        ITransactionWriteOnlyRepository transactionRepository,
        ICategoryWriteOnlyRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputTransaction> ExecuteAsync(int id, InputTransaction input)
    {
        var transaction = await _transactionRepository.FindByIdAsync(id);
        if (transaction is null)
            throw new NotFoundException(ResourceErrorMessages.TRANSACTION_NOT_FOUND);

        Validate(input);

        var category = await _categoryRepository.FindByIdAsync(input.CategoryId);
        if (category is null)
            throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        transaction.ApplyChanges(input.Type, input.Amount, input.Date, category, input.Description);

        _transactionRepository.Update(transaction);
        await _unitOfWork.Commit();

        // [TODO] Mapper
        return new OutputTransaction
        {
            Id = transaction.Id,
            AccountId = transaction.Account.Id,
            Type = transaction.Type,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
            Category = transaction.Category.Name
        };
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
