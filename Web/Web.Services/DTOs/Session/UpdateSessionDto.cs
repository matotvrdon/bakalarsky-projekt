namespace Web.Services.DTOs.Session;

public class UpdateSessionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}