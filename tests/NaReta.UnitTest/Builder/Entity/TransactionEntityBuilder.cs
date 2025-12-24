using Bogus;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;

namespace NaReta.UnitTest.Builder.Entity;

internal class TransactionEntityBuilder
{
    private static readonly Faker<Transaction> _faker = new Faker<Transaction>();

    public static Transaction Build(Account account, Category category, TransactionType? type = null)
    {
        return _faker.CustomInstantiator(f => new Transaction(
                    account,
                    type ?? f.PickRandom<TransactionType>(),
                    f.Finance.Amount(1),
                    f.Date.Past(),
                    category,
                    f.Commerce.Product()
                )).Generate();
    }

    public static List<Transaction> Build(Account account, Category category, int count, TransactionType? type = null)
    {
        return _faker.CustomInstantiator(f => new Transaction(
                    account,
                    type ?? f.PickRandom<TransactionType>(),
                    f.Finance.Amount(1),
                    f.Date.Past(),
                    category,
                    f.Commerce.Product()
                )).Generate(count);
    }
}
