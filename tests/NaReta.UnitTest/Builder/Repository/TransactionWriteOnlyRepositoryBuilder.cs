using Moq;
using NaReta.Domain.Repositories.Transactions;

namespace NaReta.UnitTest.Builder.Repository;

internal class TransactionWriteOnlyRepositoryBuilder
{
    public static ITransactionWriteOnlyRepository Build()
    {
        var mock = new Mock<ITransactionWriteOnlyRepository>();
        return mock.Object;
    }
}
