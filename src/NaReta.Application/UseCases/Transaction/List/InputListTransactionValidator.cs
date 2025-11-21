using FluentValidation;
using NaReta.Common;

namespace NaReta.Application.UseCases.Transaction.List;

public class InputListTransactionValidator : AbstractValidator<InputListTransaction>
{
    public InputListTransactionValidator()
    {
        RuleFor(t => t.StartDate).LessThanOrEqualTo(t => t.EndDate)
             .WithMessage(ResourceErrorMessages.START_DATE_LESS_THAN_END_DATE);
    }
}
