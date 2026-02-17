using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class SubmissionCreateDtoValidator : AbstractValidator<SubmissionCreateDto>
{
    public SubmissionCreateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Abstract).NotEmpty();
        RuleFor(x => x.Authors).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Affiliation).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
    }
}
