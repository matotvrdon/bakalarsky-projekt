namespace Web.Services.DTOs.Theme;

public class CreateThemeDto
{
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Chair { get; set; }

    public int SessionId { get; set; }
}