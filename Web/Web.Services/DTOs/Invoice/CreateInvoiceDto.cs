namespace Web.Services.DTOs.Invoice;

public class CreateInvoiceDto
{
    public int SupplierId { get; set; }
    public int CustomerId { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DueDate { get; set; }
}