using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Theme;

public class CreateThemeDto
{
    [Required]
    public required string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    [Required]
    public required string Chair { get; set; }

    [Range(1, int.MaxValue)]
    public int SessionId { get; set; }
}
