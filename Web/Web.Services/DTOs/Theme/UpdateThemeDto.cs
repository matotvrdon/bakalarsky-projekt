using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Theme;

public class UpdateThemeDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    [Required]
    public required string Chair { get; set; }
}
