namespace Web.Services.DTOs;

public class BookingOptionsUpdateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Price { get; set; }
}
