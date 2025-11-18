using Web.Services.DTOs.Attendee;
using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.DTOs.Customer;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Ico { get; set; }
    public string Dic { get; set; }
    public string IcDph { get; set; }
    
    public List<AttendeeDto> Attendee { get; set; }
}