using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Talk;

public class UpdateTalkDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Content { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
