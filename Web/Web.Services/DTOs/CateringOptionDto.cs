namespace Web.Services.DTOs;

public class CateringOptionDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool PerDay { get; set; }
    public bool Active { get; set; }
}
