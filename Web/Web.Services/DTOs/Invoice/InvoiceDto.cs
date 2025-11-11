using Web.Services.DTOs.Attendee;
using Web.Services.DTOs.Customer;
using Web.Services.DTOs.InvoiceItem;
using Web.Services.DTOs.Supplier;

namespace Web.Services.DTOs.Invoice;

public class InvoiceDto
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalPrice { get; set; }
    
    public CustomerDto Customer { get; set; }
    public SupplierDto Supplier { get; set; }
    public List<AttendeeDto> Attendee { get; set; }
}