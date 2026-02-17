using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class InvitationSendRequestDtoValidator : AbstractValidator<InvitationSendRequestDto>
{
    public InvitationSendRequestDtoValidator()
    {
        RuleFor(x => x.Emails).NotEmpty();
        RuleForEach(x => x.Emails).EmailAddress();
        RuleFor(x => x.Subject).NotEmpty();
        RuleFor(x => x.Body).NotEmpty();
    }
}
