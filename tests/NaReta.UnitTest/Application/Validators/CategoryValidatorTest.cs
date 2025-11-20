using NaReta.Application.UseCases.Category;
using NaReta.Common;
using NaReta.UnitTest.Builder.Input;
using Shouldly;

namespace NaReta.UnitTest.Application.Validators;

[Trait("Application", "Validator - Category")]
public class CategoryValidatorTest
{
    [Theory(DisplayName = nameof(Validate_WhenNameEmpty_RetursErrorMessageRequired))]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Validate_WhenNameEmpty_RetursErrorMessageRequired(string name)
    {
        var input = InputCategoryBuilder.Build();
        input.Name = name;

        var validator = new CategoryValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Theory(DisplayName = nameof(Validate_WhenNameLessThanLimitCaracter_RetursErrorMessageRequired))]
    [InlineData("A")]
    [InlineData("AA")]
    public void Validate_WhenNameLessThanLimitCaracter_RetursErrorMessageRequired(string name)
    {
        var input = InputCategoryBuilder.Build();
        input.Name = name;

        var validator = new CategoryValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.CATEGORY_NAME_INVALID_LENGTH));
    }

    [Theory(DisplayName = nameof(Validate_WhenNameGreatherThanLimitCaracter_RetursErrorMessageRequired))]
    [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry")]
    public void Validate_WhenNameGreatherThanLimitCaracter_RetursErrorMessageRequired(string name)
    {
        var input = InputCategoryBuilder.Build();
        input.Name = name;

        var validator = new CategoryValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(err => err.ErrorMessage.Equals(ResourceErrorMessages.CATEGORY_NAME_INVALID_LENGTH));
    }


    [Fact(DisplayName = nameof(Validate_ValidParameters_RetursTrue))]
    public void Validate_ValidParameters_RetursTrue()
    {
        var input = InputCategoryBuilder.Build();

        var validator = new CategoryValidator();
        var result = validator.Validate(input);

        result.IsValid.ShouldBeTrue();
    }
}
