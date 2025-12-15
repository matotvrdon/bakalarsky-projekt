using Web.Services.DTOs.Conference;
using Web.Services.DTOs.Session;

namespace Web.Services.DTOs.Day;

public class CreateDayDto
{
    public DateOnly Date { get; set; }

    public int ConferenceId { get; set; }
}