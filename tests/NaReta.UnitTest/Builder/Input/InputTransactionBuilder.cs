using Bogus;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Domain.Enums;

namespace NaReta.UnitTest.Builder.Input;

internal class InputTransactionBuilder
{
    public static InputTransaction Build()
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        return faker.Generate();
    }
}
