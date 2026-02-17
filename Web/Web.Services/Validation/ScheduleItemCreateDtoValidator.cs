using FluentValidation;
using Web.Services.DTOs;

namespace Web.Services.Validation;

public class ScheduleItemCreateDtoValidator : AbstractValidator<ScheduleItemCreateDto>
{
    public ScheduleItemCreateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Date).NotEmpty();
    }
}
