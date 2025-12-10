using Web.Services.DTOs.Day;

namespace Web.Services.DTOs.Conference;

public class ConferenceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public List<DayDto> Day { get; set; }
}