namespace Web.Services.DTOs.Conference;

public class CreateConferenceDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}