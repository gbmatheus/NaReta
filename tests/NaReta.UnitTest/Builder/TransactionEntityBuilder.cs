using Bogus;
using NaReta.Domain.Entities;
using NaReta.Domain.Enums;

namespace NaReta.Domain.Test.Builder;
internal class TransactionEntityBuilder
{
    public static Transaction Build(Account account, Category category, TransactionType? type = null)
    {
        return new Faker<Transaction>()
                .CustomInstantiator(f => new Transaction(
                    account,
                    type ?? f.PickRandom<TransactionType>(),
                    f.Finance.Amount(1),
                    f.Date.Past(),
                    category,
                    f.Commerce.Product()
                ));
    }
}
