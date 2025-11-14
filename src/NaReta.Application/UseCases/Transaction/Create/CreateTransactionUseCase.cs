using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Application.UseCases.Transaction.Create;
using NaReta.Domain;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;
using DomainEntity = NaReta.Domain.Entities;

namespace NaReta.Application.UseCases.Transactions.Create;

internal class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly ITransactionWriteOnlyRepository _transactionRepository;
    private readonly ICategoryWriteOnlyRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionUseCase(
        ITransactionWriteOnlyRepository transactionRepository,
        ICategoryWriteOnlyRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputTransaction> ExecuteAsync(int AccountId, InputCreateTransaction input)
    {
        var category = await _categoryRepository.FindByIdAsync(input.CategoryId);
        if (category is null)
            // [TODO] Exceção BadRequest
            throw new Exception(ResourceErrorMessages.CATEGORY_NOT_EXISTS);

        var transaction = new DomainEntity.Transaction(AccountId, input.Type, input.Amount, input.Date, category, input.Description);
        await _transactionRepository.AddSync(transaction);
        await _unitOfWork.Commit();

        // [TODO] Mapper
        return new OutputTransaction
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            Type = transaction.Type,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
            Category = transaction.Category.Name
        };
    }
}
