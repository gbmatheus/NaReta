using Bogus;
using NaReta.Application.UseCases.Category;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Application.UseCases.Transaction;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common;
using NaReta.Domain.Enums;
using Shouldly;

namespace NaReta.UnitTest.Application.Validators;

[Trait("Application", "Validator - Category")]
public class TransactionValidatorTest
{
    [Fact(DisplayName = nameof(Validate_WhenAmountLessThanOrEqualToZero_ReturnErroMessageGreaterThanZero))]
    public void Validate_WhenAmountLessThanOrEqualToZero_ReturnErroMessageGreaterThanZero()
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        input.Amount = new Faker().Finance.Amount(min: -1000, 0);

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_GREATER_THAN_ZERO));
    }

    [Fact(DisplayName = nameof(Validate_WhenTypeInvalid_ReturnErroMessageTypeInvalid))]
    public void Validate_WhenTypeInvalid_ReturnErroMessageTypeInvalid()
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        input.Type = (TransactionType)new Faker().Random.Number(10000);

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.TYPE_INVALID));
    }

    [Fact(DisplayName = nameof(Validate_WhenDateLessThanLimit_ReturnErroMessageDateInvalid))]
    public void Validate_WhenDateLessThanLimit_ReturnErroMessageDateInvalid()
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        input.Date = new Faker().Date.Past(1, refDate: new DateTime(2000, 1, 1));

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.DATE_INVALID));
    }

    [Theory(DisplayName = nameof(Validate_WhenNameEmpty_RetursErrorMessageInvalid))]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Validate_WhenNameEmpty_RetursErrorMessageInvalid(string description)
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        input.Description = description;

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.DESCRIPTION_INVALID));
    }

    [Theory(DisplayName = nameof(Validate_WhenNameLessThanLimitCaracter_RetursErrorMessageRequired))]
    [InlineData("A")]
    [InlineData("AA")]
    public void Validate_WhenNameLessThanLimitCaracter_RetursErrorMessageRequired(string description)
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        input.Description = description;

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.DESCRIPTION_INVALID_LENGTH));
    }

    [Fact(DisplayName = nameof(Validate_WhenDescriptionGreatherThanLimitCaracter_RetursErrorMessageRequired))]
    public void Validate_WhenDescriptionGreatherThanLimitCaracter_RetursErrorMessageRequired()
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        input.Description = new Faker().Lorem.Text();

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.DESCRIPTION_INVALID_LENGTH));
    }

    [Fact(DisplayName = nameof(Validate_ValidParameters_ReturnTrue))]
    public void Validate_ValidParameters_ReturnTrue()
    {
        var faker = new Faker<InputTransaction>()
            .RuleFor(t => t.Amount, f => f.Finance.Amount(min: 1))
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Date, f => f.Date.Between(DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddYears(1)))
            .RuleFor(t => t.Description, f => f.Commerce.Product())
            .RuleFor(t => t.CategoryId, f => f.Random.Number(1, 5));
        var input = faker.Generate();

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeTrue();
    }
}
