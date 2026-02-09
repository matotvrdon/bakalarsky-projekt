namespace Web.Domain.Models;

public class Invoice
{
    public int Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalPrice { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
