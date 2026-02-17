using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class CateringOptionDtoValidator : AbstractValidator<CateringOptionDto>
{
    public CateringOptionDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
