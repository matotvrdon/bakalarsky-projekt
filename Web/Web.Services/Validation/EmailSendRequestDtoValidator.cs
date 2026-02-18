using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class EmailSendRequestDtoValidator : AbstractValidator<EmailSendRequestDto>
{
    public EmailSendRequestDtoValidator()
    {
        RuleFor(x => x.To).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
