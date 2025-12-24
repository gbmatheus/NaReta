using FluentValidation;
using NaReta.Common;

namespace NaReta.Application.UseCases.Transaction.List;

public class InputListTransactionValidator : AbstractValidator<InputListTransaction>
{
    public InputListTransactionValidator()
    {
        RuleFor(t => t.AccountId).GreaterThan((int)uint.MinValue)
             .WithMessage(ResourceErrorMessages.ACCOUNT_INVALID);
        RuleFor(t => t.StartDate).LessThanOrEqualTo(t => t.EndDate)
             .WithMessage(ResourceErrorMessages.START_DATE_LESS_THAN_END_DATE);
        RuleFor(t => t.PageNumber).GreaterThan((int)uint.MinValue)
            .WithMessage(ResourceErrorMessages.PAGE_NUMBER_GREATER_THAN_ZERO);
        RuleFor(t => t.PageSize).LessThanOrEqualTo(100)
            .WithMessage(ResourceErrorMessages.ITEM_PER_PAGE_EXCEEDED);
    }
}
