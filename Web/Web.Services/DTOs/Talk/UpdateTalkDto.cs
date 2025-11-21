namespace Web.Services.DTOs.Talk;

public class UpdateTalkDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}