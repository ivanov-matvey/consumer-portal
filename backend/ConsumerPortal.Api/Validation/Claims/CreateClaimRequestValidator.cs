using ConsumerPortal.Api.Contracts.Claims;
using ConsumerPortal.Api.Validation.Common;
using FluentValidation;

namespace ConsumerPortal.Api.Validation.Claims;

public class CreateClaimRequestValidator : AbstractValidator<CreateClaimRequest>
{
    public CreateClaimRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Укажите заголовок жалобы.")
            .Length(10, 150).WithMessage("Заголовок должен содержать от 10 до 150 символов.");

        RuleFor(request => request.Text)
            .NotEmpty().WithMessage("Укажите текст жалобы.");

        RuleFor(request => request.CompanyId)
            .NotEmpty().WithMessage("Выберите организацию.");

        RuleFor(request => request.Inn)
            .SetValidator(new InnValidator());

        RuleFor(request => request.UserId)
            .NotEmpty().WithMessage("Не указан автор жалобы.");
    }
}
