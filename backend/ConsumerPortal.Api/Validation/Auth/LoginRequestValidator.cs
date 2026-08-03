using ConsumerPortal.Api.Contracts.Auth;
using FluentValidation;

namespace ConsumerPortal.Api.Validation.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("Укажите email.")
            .EmailAddress().WithMessage("Укажите корректный email.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Укажите пароль.");
    }
}
