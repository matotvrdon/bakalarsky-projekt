using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class InvoiceItemDtoValidator : AbstractValidator<InvoiceItemDto>
{
    public InvoiceItemDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
