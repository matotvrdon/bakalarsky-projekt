using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.DTOs.Attendee;

public class AttendeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public int? CustomerId { get; set; }
    
    public List<InvoiceItemDto?> InvoiceItem { get; set; }
}