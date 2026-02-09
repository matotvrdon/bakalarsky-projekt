using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Session;

public class CreateSessionDto
{
    [Required]
    public required string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    [Range(1, int.MaxValue)]
    public int DayId { get; set; }
}
