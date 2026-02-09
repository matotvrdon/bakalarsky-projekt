namespace Web.Domain.Models;

public class Theme
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public required string Chair { get; set; }
    
    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;
    
    public List<Talk> Talk { get; set; } = new();
}
