using FluentValidation;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Common;

namespace NaReta.Application.UseCases.Transaction;

public class TransactionValidator : AbstractValidator<InputTransaction>
{
    public TransactionValidator()
    {
        RuleFor(transaction => transaction.Amount)
            .GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.AMOUNT_EQUAL_OR_LESS_ZERO);
        RuleFor(transaction => transaction.Type).IsInEnum().WithMessage(ResourceErrorMessages.TYPE_INVALID);
        RuleFor(transaction => transaction.Date)
            .GreaterThan(new DateTime(2000, 1, 1)).LessThan(DateTime.UtcNow.AddYears(5))
            .WithMessage(ResourceErrorMessages.DATE_INVALID);
        RuleFor(transaction => transaction.Description)
            .NotEmpty().WithMessage(ResourceErrorMessages.DESCRIPTION_INVALID)
            .Length(1, 255).WithMessage(ResourceErrorMessages.DESCRIPTION_INVALID_LENGTH)
                .When(transaction => string.IsNullOrWhiteSpace(transaction.Description), ApplyConditionTo.CurrentValidator);
    }
}
