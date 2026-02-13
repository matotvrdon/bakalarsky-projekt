namespace Web.Services.DTOs;

public class AccommodationOptionDto
{
    public int Id { get; set; }
    public required string Hotel { get; set; }
    public string? Address { get; set; }
    public required string RoomType { get; set; }
    public decimal Price { get; set; }
    public int Total { get; set; }
    public int Available { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> Amenities { get; set; } = new();
}
