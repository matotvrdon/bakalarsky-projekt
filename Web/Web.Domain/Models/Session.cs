namespace Web.Domain.Models;

public class Session
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public int DayId { get; set; }
    public Day Day { get; set; } = null!;
    
    public List<Theme> Theme { get; set; } = new();
}
