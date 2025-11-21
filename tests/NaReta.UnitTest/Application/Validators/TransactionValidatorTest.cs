using Bogus;
using NaReta.Application.UseCases.Transaction;
using NaReta.Common;
using NaReta.Domain.Enums;
using NaReta.UnitTest.Builder.Input;
using Shouldly;

namespace NaReta.UnitTest.Application.Validators;

[Trait("Application", "Validator - Trasaction")]
public class TransactionValidatorTest
{
    [Fact(DisplayName = nameof(Validate_WhenAmountLessThanOrEqualToZero_ReturnErroMessageGreaterThanZero))]
    public void Validate_WhenAmountLessThanOrEqualToZero_ReturnErroMessageGreaterThanZero()
    {
        var input = InputTransactionBuilder.Build();
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
        var input = InputTransactionBuilder.Build();
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
        var input = InputTransactionBuilder.Build();
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
        var input = InputTransactionBuilder.Build();
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
        var input = InputTransactionBuilder.Build();
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
        var input = InputTransactionBuilder.Build();
        input.Description = new Faker().Lorem.Paragraphs(4);

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.DESCRIPTION_INVALID_LENGTH));
    }

    [Fact(DisplayName = nameof(Validate_ValidParameters_ReturnTrue))]
    public void Validate_ValidParameters_ReturnTrue()
    {
        var input = InputTransactionBuilder.Build();

        var validator = new TransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeTrue();
    }
}
