using Web.Domain.Enums;

namespace Web.Domain.Models;

public class ContentPage
{
	public int Id { get; set; }
	public string Slug { get; set; } = null!;
	public string Title { get; set; } = null!;
	
	public string DraftJson { get; set; } = "[]";
	public string? PublishedJson { get; set; }

	public ContentStatus Status { get; set; }

	public DateTime UpdatedUtc { get; set; }
	public DateTime? PublishedUtc { get; set; }
}