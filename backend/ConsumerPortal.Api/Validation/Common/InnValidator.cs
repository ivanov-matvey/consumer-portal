using FluentValidation;

namespace ConsumerPortal.Api.Validation.Common;

public class InnValidator : AbstractValidator<string>
{
    public InnValidator()
    {
        RuleFor(inn => inn)
            .NotEmpty().WithMessage("Укажите ИНН.")
            .Matches("^(?:[0-9]{10}|[0-9]{12})$")
            .WithMessage("ИНН должен состоять из 10 или 12 цифр.");
    }
}
