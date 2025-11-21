using Moq;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.UnitTest.Builder.Repository;

internal class TransactionReadOnlyRepositoryBuilder
{
    Mock<ITransactionReadOnlyRepository> mock;

    public TransactionReadOnlyRepositoryBuilder()
    {
        mock = new Mock<ITransactionReadOnlyRepository>();
    }

    public TransactionReadOnlyRepositoryBuilder ListByAccountIdAsync(List<Transaction> transactions)
    {
        mock.Setup(config => config.ListByAccountIdAsync(It.IsAny<int>(), null, null)).ReturnsAsync(transactions);
        return this;
    }

    public TransactionReadOnlyRepositoryBuilder ListByAccountIdWithDateAsync(InputListTransaction input, List<Transaction> transactions)
    {
        mock.Setup(config => config.ListByAccountIdAsync(It.IsAny<int>(), input.StartDate, input.EndDate))
            .ReturnsAsync(() =>
            {
                var result = transactions.Where(t => t.Date >= input.StartDate && t.Date <= input.EndDate).ToList();
                return transactions.Where(t => t.Date >= input.StartDate && t.Date <= input.EndDate).ToList();
            });
        return this;
    }

    public ITransactionReadOnlyRepository Build()
    {
        return mock.Object;
    }
}
