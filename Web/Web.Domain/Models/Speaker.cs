namespace Web.Domain.Models;

public class Speaker
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Title { get; set; }
    public string? Affiliation { get; set; }
    public string? Bio { get; set; }
    public string? Topics { get; set; }
    public string? ImageUrl { get; set; }
    public string? Email { get; set; }
}