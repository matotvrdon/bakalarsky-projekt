using Web.Services.DTOs.Conference;
using Web.Services.DTOs.Session;

namespace Web.Services.DTOs.Day;

public class DayDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    
    public int ConferenceId { get; set; }
    public ConferenceDto Conference { get; set; }

    public List<SessionDto> Session { get; set; }
}