using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class SubmissionUpdateDtoValidator : AbstractValidator<SubmissionUpdateDto>
{
    public SubmissionUpdateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Abstract).NotEmpty();
        RuleFor(x => x.Authors).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Affiliation).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Status).IsInEnum();
    }
}
