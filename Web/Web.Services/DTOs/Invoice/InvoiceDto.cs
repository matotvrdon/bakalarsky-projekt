using Web.Services.DTOs.Attendee;
using Web.Services.DTOs.Customer;
using Web.Services.DTOs.InvoiceItem;
using Web.Services.DTOs.Supplier;

namespace Web.Services.DTOs.Invoice;

public class InvoiceDto
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal TotalPrice { get; set; }
    
    public CustomerDto Customer { get; set; }
    public SupplierDto Supplier { get; set; }
    public List<AttendeeDto> Attendee { get; set; }
}