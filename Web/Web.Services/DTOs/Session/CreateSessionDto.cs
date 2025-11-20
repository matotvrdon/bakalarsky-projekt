namespace Web.Services.DTOs.Session;

public class CreateSessionDto
{
    public string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int DayId { get; set; }
}