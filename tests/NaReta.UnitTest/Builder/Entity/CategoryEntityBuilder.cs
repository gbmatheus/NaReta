using Bogus;
using NaReta.Domain.Entities;

namespace NaReta.UnitTest.Builder.Entity;
internal class CategoryEntityBuilder
{
    public static Category Build()
    {
        return new Faker<Category>().CustomInstantiator(f => new Category(f.Name.JobTitle()));
    }

    public static List<Category> BuildCollection(int size)
    {
        return new Faker<Category>().CustomInstantiator(f => new Category(f.Name.JobTitle())).Generate(size);
    }
}
