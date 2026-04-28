using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class RegistrationBasicRequestDtoValidator : AbstractValidator<RegistrationBasicRequestDto>
{
    public RegistrationBasicRequestDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.ConferenceId).GreaterThan(0);
    }
}
