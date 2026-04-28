using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class RegistrationAccountRequestDtoValidator : AbstractValidator<RegistrationAccountRequestDto>
{
    public RegistrationAccountRequestDtoValidator()
    {
        RuleFor(x => x.ParticipantId).GreaterThan(0);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password).WithMessage("ConfirmPassword must match Password.");
    }
}
