using Moq;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Accounts;

namespace NaReta.UnitTest.Builder.Repository;

internal class AccountWriteOnlyRepositoryBuilder
{
    Mock<IAccountWriteOnlyRepository> mock;

    public AccountWriteOnlyRepositoryBuilder()
    {
        mock = new Mock<IAccountWriteOnlyRepository>();
    }

    public AccountWriteOnlyRepositoryBuilder FindByIdAsync(Account account)
    {
        mock.Setup(config => config.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(account);
        return this;
    }

    public IAccountWriteOnlyRepository Build()
    {
        return mock.Object;
    }
}
