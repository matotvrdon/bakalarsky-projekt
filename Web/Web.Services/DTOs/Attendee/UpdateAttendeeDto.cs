using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Attendee;

public class UpdateAttendeeDto
{
    [Required]
    [MinLength(1)]
    public List<int> AttendeeId { get; set; } = new();

    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
}
