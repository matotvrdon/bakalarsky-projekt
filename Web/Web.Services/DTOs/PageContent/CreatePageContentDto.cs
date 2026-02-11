using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.PageContent;

public class CreatePageContentDto
{
    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Markdown { get; set; }

    [Range(1, int.MaxValue)]
    public int NavBarMenuId { get; set; }
}
