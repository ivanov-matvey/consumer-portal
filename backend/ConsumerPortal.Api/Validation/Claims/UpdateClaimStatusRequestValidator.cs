using ConsumerPortal.Api.Contracts.Claims;
using ConsumerPortal.Api.Domain.Enums;
using FluentValidation;

namespace ConsumerPortal.Api.Validation.Claims;

public class UpdateClaimStatusRequestValidator : AbstractValidator<UpdateClaimStatusRequest>
{
    public UpdateClaimStatusRequestValidator()
    {
        RuleFor(request => request.Status)
            .Must(status => Enum.IsDefined(typeof(ClaimStatus), status))
            .WithMessage("Укажите корректный статус жалобы.");
    }
}
