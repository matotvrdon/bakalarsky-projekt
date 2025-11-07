namespace Web.Domain.Models;

public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<InvoiceItem> InvoiceItem { get; set; }
}