using Bogus;
using NaReta.Domain.Entities;

namespace NaReta.Domain.Test.Builder;
internal class CategoryEntityBuilder
{
    public static Category Build()
    {
        return new Faker<Category>().CustomInstantiator(f => new Category(f.Name.JobTitle()));
    }
}
