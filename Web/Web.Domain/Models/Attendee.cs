namespace Web.Domain.Models;

public class Attendee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public List<InvoiceItem> InvoiceItem { get; set; } = new();
}
