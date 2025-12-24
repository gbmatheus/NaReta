using Bogus;
using NaReta.Domain.Entities;

namespace NaReta.UnitTest.Builder.Entity;

internal class AccountEntityBuilder
{
    private static readonly Faker<Account> _faker = new Faker<Account>()
        .CustomInstantiator(f => new Account(f.Person.FirstName, f.Person.Email));

    public static Account Build()
    {
        return _faker.Generate();
    }
}
