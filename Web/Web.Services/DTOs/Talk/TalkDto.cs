namespace Web.Services.DTOs.Talk;

public class TalkDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public TimeOnly StrartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}