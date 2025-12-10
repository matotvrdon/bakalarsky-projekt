namespace Web.Services.DTOs.Conference;

public class CreateConferenceDto
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}