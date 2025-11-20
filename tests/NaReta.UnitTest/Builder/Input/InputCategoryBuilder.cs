using Bogus;
using NaReta.Application.UseCases.Category._Common;

namespace NaReta.UnitTest.Builder.Input;

internal class InputCategoryBuilder
{
    public static InputCategory Build()
    {
        var faker = new Faker<InputCategory>()
            .RuleFor(c => c.Name, f => f.Finance.AccountName());
        return faker.Generate();
    }
}
