using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class RegistrationSimpleRequestDtoValidator : AbstractValidator<RegistrationSimpleRequestDto>
{
    public RegistrationSimpleRequestDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
