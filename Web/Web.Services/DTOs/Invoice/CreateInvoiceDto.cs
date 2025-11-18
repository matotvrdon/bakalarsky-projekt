namespace Web.Services.DTOs.Invoice;

public class CreateInvoiceDto
{
    public int SupplierId { get; set; }
    public int CustomerId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}