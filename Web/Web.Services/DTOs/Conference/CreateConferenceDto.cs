using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Conference;

public class CreateConferenceDto
{
    [Required]
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
