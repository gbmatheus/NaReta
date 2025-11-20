using Bogus;
using NaReta.Application.UseCases.Account.Create;

namespace NaReta.UnitTest.Builder.Input;

internal class InputAccountBuilder
{
    public static InputCreateAccount Build()
    {
        var faker = new Faker<InputCreateAccount>()
            .RuleFor(c => c.Name, f => f.Person.UserName);
        return faker.Generate();
    }
}
