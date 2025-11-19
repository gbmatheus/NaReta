using FluentValidation;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Common;

namespace NaReta.Application.UseCases.Category;

internal class CategoryValidator : AbstractValidator<InputCategory>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED)
            .Length(3, 50).WithMessage(ResourceErrorMessages.CATEGORY_NAME_INVALID_LENGTH)
                .When(category => string.IsNullOrWhiteSpace(category.Name), ApplyConditionTo.CurrentValidator);
    }
}
