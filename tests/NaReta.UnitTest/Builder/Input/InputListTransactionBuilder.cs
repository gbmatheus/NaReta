using Bogus;
using NaReta.Application.UseCases.Transaction.List;

namespace NaReta.UnitTest.Builder.Input;

internal class InputListTransactionBuilder
{
    public static InputListTransaction Build()
    {
        var faker = new Faker<InputListTransaction>()
            .RuleFor(t => t.AccountId, f => f.Random.Number(1, 5))
            .RuleFor(t => t.StartDate, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(-1)))
            .RuleFor(t => t.EndDate, (f, t) => f.Date.Between(t.StartDate!.Value, DateTime.UtcNow.AddYears(1)));

        return faker.Generate();
    }
}
