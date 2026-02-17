using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class SpeakerCreateDtoValidator : AbstractValidator<SpeakerCreateDto>
{
    public SpeakerCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
