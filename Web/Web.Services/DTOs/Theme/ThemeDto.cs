using Web.Services.DTOs.Session;
using Web.Services.DTOs.Talk;

namespace Web.Services.DTOs.Theme;

public class ThemeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Chair { get; set; }

    public int SessionId { get; set; }
    public SessionDto Session { get; set; }

    public List<TalkDto> Talk { get; set; }
}