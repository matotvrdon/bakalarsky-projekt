using Web.Services.DTOs.Talk;

namespace Web.Services.DTOs.Theme;

public class ThemeDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public required string Chair { get; set; }

    public int SessionId { get; set; }

    public List<TalkDto> Talk { get; set; } = new();
}
