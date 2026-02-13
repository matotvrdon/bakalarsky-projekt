namespace Web.Services.DTOs;

public class SpeakerDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Title { get; set; }
    public string? Affiliation { get; set; }
    public string? Bio { get; set; }
    public List<string> Topics { get; set; } = new();
    public string? ImageUrl { get; set; }
    public string? Email { get; set; }
}
