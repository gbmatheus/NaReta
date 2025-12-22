using Moq;
using NaReta.Domain.Entities;
using NaReta.Domain.Repositories.Categories;

namespace NaReta.UnitTest.Builder.Repository;

internal class CategoryWriteOnlyRepositoryBuilder
{
    Mock<ICategoryWriteOnlyRepository> mock;

    public CategoryWriteOnlyRepositoryBuilder()
    {
        mock = new Mock<ICategoryWriteOnlyRepository>();
    }

    public CategoryWriteOnlyRepositoryBuilder FindByIdAsync(Category category)
    {
        mock.Setup(config => config.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);
        return this;
    }

    public ICategoryWriteOnlyRepository Build()
    {
        return mock.Object;
    }
}
