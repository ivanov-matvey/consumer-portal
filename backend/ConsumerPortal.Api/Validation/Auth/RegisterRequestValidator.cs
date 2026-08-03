using ConsumerPortal.Api.Contracts.Auth;
using FluentValidation;

namespace ConsumerPortal.Api.Validation.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.FullName)
            .NotEmpty().WithMessage("Укажите ФИО.")
            .Must(fullName => !string.IsNullOrWhiteSpace(fullName))
            .WithMessage("Укажите ФИО.")
            .MaximumLength(150).WithMessage("ФИО не должно превышать 150 символов.");

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("Укажите email.")
            .EmailAddress().WithMessage("Укажите корректный email.")
            .MaximumLength(256).WithMessage("Email не должен превышать 256 символов.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Укажите пароль.")
            .MinimumLength(8).WithMessage("Пароль должен содержать не менее 8 символов.");
    }
}
