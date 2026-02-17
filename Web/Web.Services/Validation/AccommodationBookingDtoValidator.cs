using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class AccommodationBookingDtoValidator : AbstractValidator<AccommodationBookingDto>
{
    public AccommodationBookingDtoValidator()
    {
        RuleFor(x => x.ParticipantId).GreaterThan(0);
        RuleFor(x => x.OptionId).GreaterThan(0);
        RuleFor(x => x.CheckOut).GreaterThan(x => x.CheckIn);
        RuleFor(x => x.Nights).GreaterThan(0);
    }
}
