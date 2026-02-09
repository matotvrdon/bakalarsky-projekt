using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.DTOs.Attendee;

public class AttendeeDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    
    public int? CustomerId { get; set; }
    
    public List<InvoiceItemDto?> InvoiceItem { get; set; } = new();
}
