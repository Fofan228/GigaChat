using FluentValidation;

namespace GigaChat.Core.Authentication.Commands.Registration;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .MaximumLength(10)
            .MinimumLength(3);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(3);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(8);
    }
}