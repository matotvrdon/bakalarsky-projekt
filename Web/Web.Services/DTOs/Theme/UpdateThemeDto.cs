namespace Web.Services.DTOs.Theme;

public class UpdateThemeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Chair { get; set; }
}