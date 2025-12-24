using Bogus;
using NaReta.Domain.Entities;

namespace NaReta.UnitTest.Builder.Entity;

internal class CategoryEntityBuilder
{
    private static readonly Faker<Category> _faker = new Faker<Category>()
        .CustomInstantiator(f => new Category(f.Name.JobTitle()));

    public static Category Build()
    {
        return _faker.Generate();
    }

    public static List<Category> BuildCollection(int size)
    {
        return _faker.Generate(size);
    }
}
