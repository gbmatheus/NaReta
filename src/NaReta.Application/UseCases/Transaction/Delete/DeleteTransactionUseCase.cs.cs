
using NaReta.Common;
using NaReta.Common.Exceptions;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.Application.UseCases.Transaction.Delete;

internal class DeleteTransactionUseCase : IDeleteTransactionUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionUseCase(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id)
    {
        var transaction = await _unitOfWork.TransactionWriteOnlyRepository.GetByIdAsync(id);

        if (transaction is null)
            throw new NotFoundException(ResourceErrorMessages.TRANSACTION_NOT_FOUND);

        _unitOfWork.TransactionWriteOnlyRepository.Delete(transaction);
        await _unitOfWork.CommitAsync();
    }
}
