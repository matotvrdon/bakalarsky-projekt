using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class RegistrationRequestDtoValidator : AbstractValidator<RegistrationRequestDto>
{
    public RegistrationRequestDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.RegistrationType).IsInEnum();

        When(x => x.SubmitPaper, () =>
        {
            RuleFor(x => x.PaperTitle).NotEmpty();
            RuleFor(x => x.PaperAbstract).NotEmpty();
        });

        When(x => x.NeedAccommodation, () =>
        {
            RuleFor(x => x.AccommodationOptionId).NotNull();
            RuleFor(x => x.CheckIn).NotNull();
            RuleFor(x => x.CheckOut).NotNull();
        });
    }
}
