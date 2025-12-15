namespace Web.Domain.Models;

public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal TotalPrice { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}