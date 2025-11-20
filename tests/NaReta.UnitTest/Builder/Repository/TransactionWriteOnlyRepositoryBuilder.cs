using Moq;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.UnitTest.Builder.Repository;

internal class TransactionWriteOnlyRepositoryBuilder
{
    //Mock<ITransactionWriteOnlyRepository> mock;

    //public TransactionWriteOnlyRepositoryBuilder()
    //{
    //    mock = new Mock<ITransactionWriteOnlyRepository>();
    //}

    //public TransactionWriteOnlyRepositoryBuilder FindByIdAsync()
    //{
    //    mock.Setup(config => config.FindByIdAsync)
    //    return this;
    //}

    public static ITransactionWriteOnlyRepository Build()
    {
        var mock = new Mock<ITransactionWriteOnlyRepository>();
        return mock.Object;
    }
}
