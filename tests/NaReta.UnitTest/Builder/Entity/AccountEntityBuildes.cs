using Bogus;
using NaReta.Domain.Entities;

namespace NaReta.UnitTest.Builder.Entity;
internal class AccountEntityBuildes
{
    public static Account Build()
    {
        return new Faker<Account>().CustomInstantiator(f => new Account(f.Person.FirstName));
    }
}
