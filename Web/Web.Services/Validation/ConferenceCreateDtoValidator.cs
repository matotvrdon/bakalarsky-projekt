using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class ConferenceCreateDtoValidator : AbstractValidator<ConferenceCreateDto>
{
    public ConferenceCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Meno je povinné.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Popis je povinný.");
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Dátum konca musí býť neskôr ako dátum začiatku.");
    }
}
