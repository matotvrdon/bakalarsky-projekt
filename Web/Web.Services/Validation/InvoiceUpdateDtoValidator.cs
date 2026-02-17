using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class InvoiceUpdateDtoValidator : AbstractValidator<InvoiceUpdateDto>
{
    public InvoiceUpdateDtoValidator()
    {
        RuleFor(x => x.Status).IsInEnum();
        RuleForEach(x => x.Items).SetValidator(new InvoiceItemDtoValidator());
    }
}
