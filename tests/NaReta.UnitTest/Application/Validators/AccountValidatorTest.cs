using NaReta.Application.UseCases.Account.Create;
using NaReta.Common;
using NaReta.UnitTest.Builder.Input;
using Shouldly;

namespace NaReta.UnitTest.Application.Validators;

[Trait("Application", "Validator - Account")]
public class AccountValidatorTest
{
    [Theory(DisplayName = nameof(Validate_WhenNameEmpty_ReturnErrorMessageRequired))]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Validate_WhenNameEmpty_ReturnErrorMessageRequired(string name)
    {
        var input = InputAccountBuilder.Build();
        input.Name = name;

        var validator = new CreateAccountValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Theory(DisplayName = nameof(Validate_WhenEmailEmpty_ReturnErrorMessageRequired))]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Validate_WhenEmailEmpty_ReturnErrorMessageRequired(string email)
    {
        var input = InputAccountBuilder.Build();
        input.Email = email;

        var validator = new CreateAccountValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_REQUIRED));
    }

    [Fact(DisplayName = nameof(Validate_WhenEmailInvalid_ReturnErrorMessageInvalid))]
    public void Validate_WhenEmailInvalid_ReturnErrorMessageInvalid()
    {
        var input = InputAccountBuilder.Build();
        input.Email = input.Name;

        var validator = new CreateAccountValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
    }

    [Fact(DisplayName = nameof(Validate_ValidParamters_ReturnTrue))]
    public void Validate_ValidParamters_ReturnTrue()
    {
        var input = InputAccountBuilder.Build();

        var validator = new CreateAccountValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeTrue();
    }
}
