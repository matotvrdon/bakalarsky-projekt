using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.PageContent;

public class UpdatePageContentDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Markdown { get; set; }
}
