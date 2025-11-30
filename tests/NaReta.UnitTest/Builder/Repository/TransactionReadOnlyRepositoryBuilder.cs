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
        mock.Setup(config => config.ListByAccountIdAsync(It.IsAny<int>(), null, null, It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(transactions);
        return this;
    }

    public TransactionReadOnlyRepositoryBuilder ListByAccountIdWithDateAsync(InputListTransaction input, List<Transaction> transactions)
    {
        var query = transactions.AsQueryable();
        if (input.StartDate != null && input.EndDate != null)
            query = query.Where(t => t.Date >= input.StartDate && t.Date <= input.EndDate);

        var result = query.Take(input.ItemPerPage).ToList();
        
        mock.Setup(config => config.ListByAccountIdAsync(
            It.IsAny<int>(),
            input.StartDate, input.EndDate, It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }

    public ITransactionReadOnlyRepository Build()
    {
        return mock.Object;
    }
}
