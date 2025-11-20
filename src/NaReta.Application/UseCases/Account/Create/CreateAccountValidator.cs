using FluentValidation;
using NaReta.Common;

namespace NaReta.Application.UseCases.Account.Create;

public class CreateAccountValidator : AbstractValidator<InputCreateAccount>
{
    public CreateAccountValidator()
    {
        RuleFor(input => input.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_REQUIRED);
    }
}
