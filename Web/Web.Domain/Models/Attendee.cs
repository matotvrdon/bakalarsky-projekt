namespace Web.Domain.Models;

public class Attendee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public List<InvoiceItem> InvoiceItem { get; set; }
}