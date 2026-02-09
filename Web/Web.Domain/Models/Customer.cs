namespace Web.Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public string? Ico { get; set; }
    public string? Dic { get; set; }
    public string? IcDph { get; set; }
    
    public List<Attendee?> Attendee { get; set; } = new();
}
