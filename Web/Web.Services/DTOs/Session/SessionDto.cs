using Web.Services.DTOs.Day;
using Web.Services.DTOs.Invoice;
using Web.Services.DTOs.Theme;

namespace Web.Services.DTOs.Session;

public class SessionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int DayId { get; set; }
    public DayDto Day { get; set; }

    public List<ThemeDto> Theme { get; set; }
}