namespace Web.Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string? Ico { get; set; }
    public string? Dic { get; set; }
    public string? IcDph { get; set; }
    
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public List<Attendee?> Attendee { get; set; }
}