using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class InvoiceCreateDtoValidator : AbstractValidator<InvoiceCreateDto>
{
    public InvoiceCreateDtoValidator()
    {
        RuleFor(x => x.ParticipantIds).NotEmpty();
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new InvoiceItemDtoValidator());
    }
}
