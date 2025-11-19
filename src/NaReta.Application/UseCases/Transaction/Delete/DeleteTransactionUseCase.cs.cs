
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Transaction.Delete;

internal class DeleteTransactionUseCase : IDeleteTransactionUseCase
{
    private readonly ITransactionWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionUseCase(
        ITransactionWriteOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id)
    {
        var transaction = await _repository.FindByIdAsync(id);

        if (transaction is null)
            throw new NotFoundException(ResourceErrorMessages.TRANSACTION_NOT_FOUND);

        _repository.Remove(transaction);
        await _unitOfWork.Commit();
    }
}
