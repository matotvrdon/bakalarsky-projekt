using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class CouponUpdateDtoValidator : AbstractValidator<CouponUpdateDto>
{
    public CouponUpdateDtoValidator()
    {
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Value).GreaterThan(0);
        RuleFor(x => x.ValidTo).GreaterThanOrEqualTo(x => x.ValidFrom);
        RuleFor(x => x.MaxUses).GreaterThan(0).When(x => x.MaxUses.HasValue);
    }
}
