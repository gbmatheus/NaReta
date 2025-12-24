using Moq;
using NaReta.Domain.Repositories;
using NaReta.Domain.Repositories.Accounts;
using NaReta.Domain.Repositories.Categories;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.UnitTest.Builder.Repository;

internal class UnitOfWorkBuilder
{
    private readonly Mock<IUnitOfWork> mock;

    public UnitOfWorkBuilder()
    {
        mock = new Mock<IUnitOfWork>();
    }

    public UnitOfWorkBuilder SetupAccountRepository(IAccountWriteOnlyRepository accountRepository)
    {
        mock.Setup(config => config.AccountWriteOnlyRepository).Returns(accountRepository);
        return this;
    }

    public UnitOfWorkBuilder SetupCategoryRepository(ICategoryWriteOnlyRepository categoryRepository)
    {
        mock.Setup(config => config.CategoryWriteOnlyRepository).Returns(categoryRepository);
        return this;
    }
    public UnitOfWorkBuilder SetupTransactionRepository(ITransactionWriteOnlyRepository transactionRepository)
    {
        mock.Setup(config => config.TransactionWriteOnlyRepository).Returns(transactionRepository);
        return this;
    }

    public IUnitOfWork Build() => mock.Object;
}
