namespace Web.Domain.Models;

public class PageContent
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Markdown { get; set; }
    public required string Html { get; set; }

    public int NavBarMenuId { get; set; }
    public NavBarMenu NavBarMenu { get; set; } = null!;
}
