namespace Web.Domain.Models;

public class Talk
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public int ThemeId { get; set; }
    public Theme Theme { get; set; }
}