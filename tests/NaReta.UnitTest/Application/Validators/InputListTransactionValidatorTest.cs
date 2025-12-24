using Bogus;
using NaReta.Application.UseCases.Transaction;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Common;
using NaReta.Domain.Enums;
using NaReta.UnitTest.Builder.Input;
using Shouldly;

namespace NaReta.UnitTest.Application.Validators;

[Trait("Application", "Validator - Trasaction")]
public class InputListTransactionValidatorTest
{
    [Fact(DisplayName = nameof(Validate_ValidParameters_ReturnsTrue))]
    public void Validate_ValidParameters_ReturnsTrue()
    {
        var input = InputListTransactionBuilder.Build();

        var validator = new InputListTransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeTrue();
    }
    
    [Fact(DisplayName = nameof(Validate_WhenStartDateGreaterThanEndDate_ReturnErroMessageDateRangeInvalid))]
    public void Validate_WhenAccountNotInformed_ReturnErroMessageAccountInvalid()
    {
        var input = InputListTransactionBuilder.Build();
        input.AccountId = 0;

        var validator = new InputListTransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();

        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.ACCOUNT_INVALID));
    }

    [Fact(DisplayName = nameof(Validate_WhenStartDateGreaterThanEndDate_ReturnErroMessageDateRangeInvalid))]
    public void Validate_WhenStartDateGreaterThanEndDate_ReturnErroMessageDateRangeInvalid()
    {
        var input = InputListTransactionBuilder.Build();
        input.EndDate = input.StartDate!.Value.AddDays(-1);

        var validator = new InputListTransactionValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();

        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.START_DATE_LESS_THAN_END_DATE));
    }
}
