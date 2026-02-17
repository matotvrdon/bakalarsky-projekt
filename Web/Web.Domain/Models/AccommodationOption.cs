namespace Web.Domain.Models;

public class AccommodationOption
{
    public int Id { get; set; }
    public required string Hotel { get; set; }
    public string? Address { get; set; }
    public required string RoomType { get; set; }
    public decimal Price { get; set; }
    public int Total { get; set; }
    public int Available { get; set; }
    public string? ImageUrl { get; set; }
    public string? Amenities { get; set; }
}