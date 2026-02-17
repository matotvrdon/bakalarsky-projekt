namespace Web.Domain.Models;

public class CateringOption
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool PerDay { get; set; }
    public bool Active { get; set; }
}