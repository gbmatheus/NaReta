using Bogus;
using NaReta.Application.UseCases.Account.Create;

namespace NaReta.UnitTest.Builder.Input;

internal class InputAccountBuilder
{
    public static InputCreateAccount Build()
    {
        var faker = new Faker<InputCreateAccount>()
            .RuleFor(c => c.Name, f => f.Person.UserName)
            .RuleFor(c => c.Email, f => f.Person.Email);
        return faker.Generate();
    }
}
