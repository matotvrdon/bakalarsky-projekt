namespace Web.Domain.Models;

public class InvoiceItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public int? AttendeeId { get; set; }
    public Attendee? Attendee { get; set; }
}
