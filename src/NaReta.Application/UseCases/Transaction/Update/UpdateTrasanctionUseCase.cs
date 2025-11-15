using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common;
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

    public async Task<OutputTransaction> ExecuteAsync(int id, InputUpdateTransaction input)
    {
        var transaction = await _transactionRepository.FindByIdAsync(id);
        if (transaction is null)
            // [TODO] NotFound
            throw new Exception(ResourceErrorMessages.CATEGORY_NOT_EXISTS);

        var category = await _categoryRepository.FindByIdAsync(input.CategoryId);
        if (category is null)
            // [TODO] Exceção BadRequest
            throw new Exception(ResourceErrorMessages.CATEGORY_NOT_EXISTS);

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
}
