namespace Web.Services.DTOs.Talk;

public class TalkDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public int ThemeId { get; set; }
}
