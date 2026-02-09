using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Attendee;

public class CreateAttendeeDto
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [Phone]
    public required string Phone { get; set; }
}
