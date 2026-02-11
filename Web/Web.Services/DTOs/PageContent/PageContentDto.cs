namespace Web.Services.DTOs.PageContent;

public class PageContentDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Markdown { get; set; }
    public required string Html { get; set; }
    public int NavBarMenuId { get; set; }
}
