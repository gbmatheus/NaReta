using Moq;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.UnitTest.Builder.Repository;

internal class IUnitOfWorkBuilder
{
    public static IUnitOfWork Build(
        IAccountWriteOnlyRepository? accountRepository = null,
        ICategoryWriteOnlyRepository? categoryRepository = null,
        ITransactionWriteOnlyRepository? transactionRepository = null)
    {
        var mock = new Mock<IUnitOfWork>();

        if (accountRepository != null)
            mock.Setup(config => config.AccountWriteOnlyRepository).Returns(accountRepository);
        if (categoryRepository != null)
            mock.Setup(config => config.CategoryWriteOnlyRepository).Returns(categoryRepository);
        if (transactionRepository != null)
            mock.Setup(config => config.TransactionWriteOnlyRepository).Returns(transactionRepository);

        return mock.Object;
    }
}
