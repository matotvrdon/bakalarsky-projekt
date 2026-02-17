using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class CouponCreateDtoValidator : AbstractValidator<CouponCreateDto>
{
    public CouponCreateDtoValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Value).GreaterThan(0);
        RuleFor(x => x.ValidTo).GreaterThanOrEqualTo(x => x.ValidFrom);
        RuleFor(x => x.MaxUses).GreaterThan(0).When(x => x.MaxUses.HasValue);
    }
}
