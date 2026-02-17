using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class ParticipantCreateDtoValidator : AbstractValidator<ParticipantCreateDto>
{
    public ParticipantCreateDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.RegistrationType).IsInEnum();
    }
}
