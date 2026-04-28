namespace Web.Domain.Models;

public class Conference
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Location { get; set; }
    public ConferenceSettings? Settings { get; set; }
    public bool IsActive { get; set; }
}
