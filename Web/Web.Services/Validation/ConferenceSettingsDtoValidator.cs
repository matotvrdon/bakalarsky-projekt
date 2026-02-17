using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class ConferenceSettingsDtoValidator : AbstractValidator<ConferenceSettingsDto>
{
    public ConferenceSettingsDtoValidator()
    {
        RuleFor(x => x.ContactEmail)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));
        RuleFor(x => x.FeeSpeaker).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FeeParticipant).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FeeStudent).GreaterThanOrEqualTo(0);
        RuleFor(x => x.EarlyBirdPercent).InclusiveBetween(0, 100);
    }
}
