namespace Web.Domain.Models;

public class Theme
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Chair { get; set; }
    
    public int SessionId { get; set; }
    public Session Session { get; set; }
    
    public List<Talk> Talk { get; set; }
}