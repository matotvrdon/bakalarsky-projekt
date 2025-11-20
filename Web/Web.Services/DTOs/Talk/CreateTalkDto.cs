namespace Web.Services.DTOs.Talk;

public class CreateTalkDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public int ThemeId { get; set; }
}