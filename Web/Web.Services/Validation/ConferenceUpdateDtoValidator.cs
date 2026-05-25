using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class ConferenceUpdateDtoValidator : AbstractValidator<ConferenceUpdateDto>
{
    public ConferenceUpdateDtoValidator()
    {
        RuleFor(conference => conference.Name)
            .NotEmpty()
            .WithMessage("Meno je povinné.");

        RuleFor(conference => conference.Description)
            .NotEmpty()
            .WithMessage("Popis je povinný.");

        RuleFor(conference => conference.EndDate)
            .GreaterThanOrEqualTo(conference => conference.StartDate)
            .WithMessage("Dátum konca musí byť neskôr ako dátum začiatku.");

        RuleFor(conference => conference.Status)
            .IsInEnum()
            .WithMessage("Neplatný stav konferencie.");
    }
}