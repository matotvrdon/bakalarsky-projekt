using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class CateringOrderDtoValidator : AbstractValidator<CateringOrderDto>
{
    public CateringOrderDtoValidator()
    {
        RuleFor(x => x.ParticipantId).GreaterThan(0);
        RuleForEach(x => x.Items).SetValidator(new CateringOrderItemDtoValidator());
    }
}
