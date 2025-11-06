namespace Web.Services.DTOs.Invoice;

public class CreateInvoiceDto
{
    public int SupplierDtoId { get; set; }
    public int CustomerDtoId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}