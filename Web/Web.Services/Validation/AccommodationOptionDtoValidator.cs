using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class AccommodationOptionDtoValidator : AbstractValidator<AccommodationOptionDto>
{
    public AccommodationOptionDtoValidator()
    {
        RuleFor(x => x.Hotel).NotEmpty();
        RuleFor(x => x.RoomType).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Total).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Available).GreaterThanOrEqualTo(0);
    }
}
