using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class SubmissionCreateDtoValidator : AbstractValidator<SubmissionCreateDto>
{
    public SubmissionCreateDtoValidator()
    {
        RuleFor(x => x.ParticipantId).GreaterThan(0);
        RuleFor(x => x.ConferenceId).GreaterThan(0);
        RuleFor(x => x.SubmissionIdentifier).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}
