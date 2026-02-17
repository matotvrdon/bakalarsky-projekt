using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class CateringOrderItemDtoValidator : AbstractValidator<CateringOrderItemDto>
{
    public CateringOrderItemDtoValidator()
    {
        RuleFor(x => x.OptionId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
