using Moq;
using NaReta.Domain.Repositories;

namespace NaReta.UnitTest.Builder.Repository;

internal class IUnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();
        return mock.Object;
    }
}
