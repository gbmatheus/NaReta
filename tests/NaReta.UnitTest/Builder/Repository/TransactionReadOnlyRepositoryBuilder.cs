using Moq;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Transactions;
using NaReta.Domain.ValueObjects;

namespace NaReta.UnitTest.Builder.Repository;

internal class TransactionReadOnlyRepositoryBuilder
{
    Mock<ITransactionReadOnlyRepository> mock;

    public TransactionReadOnlyRepositoryBuilder()
    {
        mock = new Mock<ITransactionReadOnlyRepository>();
    }

    public TransactionReadOnlyRepositoryBuilder ListByTransactionFilterAsync(InputListTransaction input, List<Transaction> transactions, int totalCount)
    {
        var safeInput = input ?? new InputListTransaction();

        var result = new PagedResult<Transaction>(transactions, totalCount, safeInput.PageNumber, safeInput.PageSize);
        mock.Setup(
            config => config.ListByTransactionFilterAsync(It.Is<TransactionFilter>(filter =>
                filter.AccountId == safeInput.AccountId
                && filter.PageNumber == safeInput.PageNumber
                && filter.PageSize == safeInput.PageSize
                && filter.StartDate == safeInput.StartDate
                && filter.EndDate == safeInput.EndDate
            ))
        ).ReturnsAsync(result);
        return this;
    }

    public ITransactionReadOnlyRepository Build()
    {
        return mock.Object;
    }

}
