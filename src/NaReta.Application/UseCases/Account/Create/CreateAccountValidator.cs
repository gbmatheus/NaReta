using FluentValidation;
using NaReta.Common;

namespace NaReta.Application.UseCases.Account.Create;

public class CreateAccountValidator : AbstractValidator<InputCreateAccount>
{
    public CreateAccountValidator()
    {
        RuleFor(input => input.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(input => input.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ResourceErrorMessages.EMAIL_INVALID)
                .When(a => !string.IsNullOrWhiteSpace(a.Email),ApplyConditionTo.CurrentValidator);
    }
}
