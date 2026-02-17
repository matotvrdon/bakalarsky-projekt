using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class ConferenceUpdateDtoValidator : AbstractValidator<ConferenceUpdateDto>
{
    public ConferenceUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Meno je povinné.");
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Dátum konca musí býť neskôr ako dátum začiatku.");
    }
}
